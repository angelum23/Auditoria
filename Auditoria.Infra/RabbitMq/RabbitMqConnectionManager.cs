using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Auditoria.Infra.RabbitMq;

public class RabbitMqConnectionManager : IRabbitMqConnectionManager
{
    public readonly RabbitMqConfig _settings;
    private IConnection? _connection;
    private IModel? _channel;

    public RabbitMqConnectionManager(IOptions<RabbitMqConfig> options)
    {
        _settings = options.Value;
    }

    public string GetHost() => _settings.HostName;
    public string GetQueueName() => _settings.QueueName;
    
    public IModel GetChannel()
    {
        if (_channel != null)
            return _channel;

        var factory = new ConnectionFactory
        {
            HostName = _settings.HostName,
            UserName = _settings.UserName,
            Password = _settings.Password,
            DispatchConsumersAsync = true
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: _settings.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        return _channel;
    }

    public void Close()
    {
        _channel?.Close();
        _channel?.Dispose();
        _connection?.Close();
        _connection?.Dispose();
    }
}