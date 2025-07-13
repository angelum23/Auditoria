using System.ComponentModel;
using Auditoria.Dominio.Entidades;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Auditoria.Dominio.LogsAuditoria;

public class LogAuditoria : IdentificadorMongoDb
{
    [BsonElement("idtenant")]
    public int CodigoTenant { get; set; }
    
    [BsonElement("idunidade")]
    public int CodigoUnidade { get; set; }
    
    [BsonElement("entidadeauditada")]
    public string? EntidadeAuditada { get; set; }
    
    [BsonElement("usuario")]
    public BsonDocument Usuario { get; set; }
    
    [BsonElement("tipo")]
    [BsonRepresentation(BsonType.Int32)]
    public TipoLog TipoLog { get; set; }
    
    [BsonElement("dados")]
    public BsonDocument Dados { get; set; }
}

public enum TipoLog
{
    [Description("Inserção de Entidade")]
    Insercao = 1,

    [Description("Alteração de Entidade")]
    Alteracao = 2,

    [Description("Remoção de Entidade")]
    Remocao = 3,
}