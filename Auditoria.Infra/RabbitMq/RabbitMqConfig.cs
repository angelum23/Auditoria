namespace Auditoria.Infra.RabbitMq;

public class RabbitMqConfig
{
    //todo: ler dados do appsettings
    public string HostName { get; set; } = "localhost";
    public string QueueName { get; set; } = "fila_teste";
    public string? UserName { get; set; }
    public string? Password { get; set; }
}