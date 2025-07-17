using RabbitMQ.Client;
using System.Text;

namespace RabbitMqPublisher;

public class Program
{
    // Configuration
    private const string RabbitMqHost = "rabbitmq";
    private const string QueueName = "fila_teste";
    private const string UserName = "guest";
    private const string Password = "guest";

    public static void Main()
    {

        const string message = "MensagemDeTeste"; //Substitua pela mensagem desejada e de um compose up que ja vai pra fila.

        var factory = new ConnectionFactory()
        {
            HostName = RabbitMqHost,
            UserName = UserName,
            Password = Password 
        };

        IConnection? connection = null;
        IModel? channel = null;

        try
        {
            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            // Declare the queue to ensure it exists (match consumer settings)
            channel.QueueDeclare(queue: QueueName,
                                 durable: true,      
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            // Make message persistent
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true; 

            // Publish
            channel.BasicPublish(exchange: "",             
                                 routingKey: QueueName,   
                                 basicProperties: properties,
                                 body: body);

            Console.WriteLine($"Foi publicada na fila '{QueueName}' a mensagem: '{message}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
        finally
        {
            // Clean up
            try { channel?.Close(); } catch {  }
            try { channel?.Dispose(); } catch {  }
            try { connection?.Close(); } catch {  }
            try { connection?.Dispose(); } catch {  }
            Console.WriteLine("Publisher finished.");
        }
    }
}
