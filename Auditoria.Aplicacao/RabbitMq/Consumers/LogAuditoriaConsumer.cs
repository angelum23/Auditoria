using System.Text;
using Auditoria.Dominio.Auditaveis;
using Auditoria.Dominio.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Auditoria.Infra.RabbitMq;

public class LogAuditoriaConsumer : BackgroundService
{
    private readonly ILogger<LogAuditoriaConsumer> _logger;
    private readonly IServiceProvider _serviceProvider;

    public LogAuditoriaConsumer(IServiceProvider serviceProvider, ILogger<LogAuditoriaConsumer> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        
        var connectionManager = scope.ServiceProvider.GetRequiredService<IRabbitMqConnectionManager>();
        var servLogAuditoria = scope.ServiceProvider.GetRequiredService<IServLogAuditoria>();
        try
        {
            var channel = connectionManager.GetChannel();

            _logger.LogInformation($"Conectado ao RabbitMQ em {connectionManager.GetHost()}, esperando mensagens na fila '{connectionManager.GetQueueName()}'.");

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (model, eventArgs) =>
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogWarning("Cancellation solicitado antes de processar a mensagem.");
                    return;
                }
                
                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogWarning("CancellationToken detectado dentro do ProcessarMensagem.");
                    return;
                }
        
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                _logger.LogInformation($"Mensagem recebida da fila: {message}");

                try
                {
                    var log = JsonConvert.DeserializeObject<LogAuditoria>(message);
                    if (log == null)
                    {
                        throw new JsonException();
                    }

                    if (log.Id == ObjectId.Empty)
                    {
                        log.Id = ObjectId.GenerateNewId();
                    }

                    await servLogAuditoria.Inserir(log);

                    channel?.BasicAck(eventArgs.DeliveryTag, multiple: false);
            
                    await Task.Yield();
                }
                catch (JsonException ex)
                {
                    _logger.LogError(ex, "Falha ao deserializar a mensagem: {message}.", message);
                    channel?.BasicNack(eventArgs.DeliveryTag, multiple: false, false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao processar a mensagem.");
                    channel?.BasicNack(eventArgs.DeliveryTag, multiple: false, true);
                }
            };

            var consumerTag = channel.BasicConsume(
                queue: connectionManager.GetQueueName(),
                autoAck: false,
                consumer: consumer
            );
            
            cancellationToken.Register(() =>
            {
                _logger.LogInformation("CancellationToken disparado, cancelando consumer no RabbitMQ.");
                channel?.BasicCancel(consumerTag);
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Falha ao iniciar o consumidor RabbitMQ.");
        }

        return Task.CompletedTask;
    }
}