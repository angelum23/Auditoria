using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Auditoria.Infra.RabbitMq;

public class TesteConsumer : BackgroundService
{
    private readonly ILogger<TesteConsumer> _logger;
    private readonly RabbitMqConfig _settings;
    private IConnection? _connection;
    private IModel? _channel;

    public TesteConsumer(ILogger<TesteConsumer> logger, IOptions<RabbitMqConfig> options)
    {
        _logger = logger;
        _settings = options.Value;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _settings.HostName,
            UserName = _settings.UserName,
            Password = _settings.Password,
            DispatchConsumersAsync = true 
        };

        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _settings.QueueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            _logger.LogInformation($"Conectado ao RabbitMQ em {_settings.HostName}, esperando mensagens na fila  '{_settings.QueueName}'.");

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // Aqui faz alguma coisa com a mensagem
                _logger.LogInformation($"Mensagem recebida da fila: {message}");

                
                // Aqui confirma a mensagem
                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                await Task.Yield();
            };

            _channel.BasicConsume(queue: _settings.QueueName,
                                 autoAck: false,
                                 consumer: consumer);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Falha ao iniciar o consumidor RabbitMQ.");
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _logger.LogInformation("Finalizando o consumidor RabbitMQ.");
        try
        {
            _channel?.Close();
            _channel?.Dispose();
            _connection?.Close();
            _connection?.Dispose();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao finalizar o consumidor RabbitMQ.");
        }
        base.Dispose();
        GC.SuppressFinalize(this);
    }
}