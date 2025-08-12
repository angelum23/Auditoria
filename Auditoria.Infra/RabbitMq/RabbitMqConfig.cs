namespace Auditoria.Infra.RabbitMq;

public class RabbitMqConfig
{
    public string HostName { get; set; } = "localhost";
    public string QueueName { get; set; } = "fila_teste";
    public string Port { get; set; } = "5672";
    public string? UserName { get; set; }
    public string? Password { get; set; }
}