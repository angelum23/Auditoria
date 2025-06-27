using RabbitMQ.Client;

namespace Auditoria.Infra.RabbitMq;

public interface IRabbitMqConnectionManager
{
    string GetHost();
    string GetQueueName();
    IModel GetChannel();
    void Close();
}