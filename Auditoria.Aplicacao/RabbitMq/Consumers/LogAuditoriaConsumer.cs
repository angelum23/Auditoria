using System.Text;
using Auditoria.Dominio.Auditaveis;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Auditoria.Infra.RabbitMq;

public class LogAuditoriaConsumer : BackgroundService
{
    private readonly ILogger<LogAuditoriaConsumer> _logger;
    private readonly IRabbitMqConnectionManager _connectionManager;
    private readonly RabbitMqConfig _settings;
    private IModel? _channel;
    private readonly ServLogAuditoria _servLogAuditoria;

    public LogAuditoriaConsumer(
        ILogger<LogAuditoriaConsumer> logger,
        IOptions<RabbitMqConfig> options,
        ServLogAuditoria servLogAuditoria)
    {
        _logger = logger;
        _settings = options.Value;
        _connectionManager = new RabbitMqConnectionManager(_settings);
        _servLogAuditoria = servLogAuditoria;
    }

    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        try
        {
            _channel = _connectionManager.GetChannel();

            _logger.LogInformation($"Conectado ao RabbitMQ em {_settings.HostName}, esperando mensagens na fila '{_settings.QueueName}'.");

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (model, eventArgs) =>
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogWarning("Cancellation solicitado antes de processar a mensagem.");
                    return;
                }
                
                await ProcessarMensagem(eventArgs, cancellationToken);
            };

            var consumerTag = _channel.BasicConsume(
                queue: _settings.QueueName,
                autoAck: false,
                consumer: consumer
            );
            
            cancellationToken.Register(() =>
            {
                _logger.LogInformation("CancellationToken disparado, cancelando consumer no RabbitMQ.");
                _channel?.BasicCancel(consumerTag);
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Falha ao iniciar o consumidor RabbitMQ.");
        }

        return Task.CompletedTask;
    }

    private async Task ProcessarMensagem(BasicDeliverEventArgs eventArgs, CancellationToken cancellationToken)
    {
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

            await _servLogAuditoria.Inserir(log);

            ConfirmMessage(eventArgs.DeliveryTag);
            
            await Task.Yield();
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Falha ao deserializar a mensagem: {message}.", message);
            RejectMessage(eventArgs.DeliveryTag, requeue: false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar a mensagem.");
            RejectMessage(eventArgs.DeliveryTag, requeue: true);
        }
    }

    public override void Dispose()
    {
        _logger.LogInformation("Finalizando o consumidor RabbitMQ.");
        try
        {
            _connectionManager.Close();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao finalizar o consumidor RabbitMQ.");
        }
        base.Dispose();
        GC.SuppressFinalize(this);
    }
    
    private void ConfirmMessage(ulong deliveryTag) => _channel?.BasicAck(deliveryTag, multiple: false);
    private void RejectMessage(ulong deliveryTag, bool requeue) => _channel?.BasicNack(deliveryTag, multiple: false, requeue);
}