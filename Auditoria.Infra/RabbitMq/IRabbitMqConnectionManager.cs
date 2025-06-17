using RabbitMQ.Client;

namespace Auditoria.Infra.RabbitMq;

public interface IRabbitMqConnectionManager
{
    IModel GetChannel();
    void Close();
}