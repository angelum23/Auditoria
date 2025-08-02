using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Auditoria.Dominio.LogsAuditoria;

public class InserirLogAuditoriaDTO
{
    [JsonProperty("idoriginal")]
    [BsonElement("idoriginal")]
    public int CodigoOriginal { get; set; }
    
    [JsonProperty("idtenant")]
    [BsonElement("idtenant")]
    public int CodigoTenant { get; set; }
    
    [JsonProperty("idunidade")]
    [BsonElement("idunidade")]
    public int CodigoUnidade { get; set; }
    
    [JsonProperty("idusuario")]
    [BsonElement("idusuario")]
    public int CodigoUsuario { get; set; }
    
    [JsonProperty("entidadeauditada")]
    [BsonElement("entidadeauditada")]
    public string? EntidadeAuditada { get; set; }
    
    [JsonProperty("tipo")]
    [BsonElement("tipo")]
    [BsonRepresentation(BsonType.String)]
    public TipoLog TipoLog { get; set; }
    
    [JsonProperty("dados")]
    [BsonElement("dados")]
    public string DadoSerializado { get; set; } = string.Empty;
}