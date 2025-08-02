using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RabbitMqPublisher;

public class DadosSerializadosDto
{
    [JsonPropertyName("dados")] 
    public List<string> dados { get; set; } = [];
}

public class DadosFakesDto
{
    [JsonPropertyName("dados")] 
    public List<DadoFakeDto> Dados { get; set; } = [];
}

public class DadoFakeDto
{
    [JsonPropertyName("idoriginal")]
    public int CodigoOriginal { get; set; }
        
    [JsonPropertyName("idtenant")]
    public int CodigoTenant { get; set; }
    
    [JsonPropertyName("idunidade")]
    public int CodigoUnidade { get; set; }
    
    [JsonPropertyName("idusuario")]
    public int Usuario { get; set; }
    
    [JsonPropertyName("entidadeauditada")]
    public string? EntidadeAuditada { get; set; }
    
    [JsonPropertyName("tipo")]
    public string TipoLog { get; set; } = ""; //public enum TipoLog { Insercao, Alteracao, Remocao }
    
    [JsonPropertyName("dados")]
    public string? Dados { get; set; } = "";
}

public class Program
{
    // Configuration
    private const string RabbitMqHost = "rabbitmq";
    private const string QueueName = "fila_teste";
    private const string UserName = "guest";
    private const string Password = "guest";

    public static void Main()
    {
        var dadosFake = DadosFake.RecuperarDadosFake();

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
            
            // Make message persistent
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            // Declare the queue to ensure it exists (match consumer settings)
            channel.QueueDeclare(queue: QueueName,
                                 durable: true,      
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            // Publish
            dadosFake.ForEach(x => PublicarNoRabbit(x, channel, properties));
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

    private static void PublicarNoRabbit(string dados, IModel channel, IBasicProperties properties)
    {
        var body = Encoding.UTF8.GetBytes(dados);
        
        channel.BasicPublish(exchange: "",             
            routingKey: QueueName,   
            basicProperties: properties,
            body: body);
        
        Console.WriteLine($"Foi publicada na fila '{QueueName}' a mensagem: '{dados}'");
    }
}
