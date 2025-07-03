using System.ComponentModel;
using Auditoria.Dominio.Entidades;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Auditoria.Dominio.LogsAuditoria;

public class LogAuditoria : IdentificadorMongoDb
{
    [BsonElement("IdTenant")]
    public int CodigoTenant { get; set; }
    
    [BsonElement("IdUnidade")]
    public int CodigoUnidade { get; set; }
    
    [BsonElement("NomeEntidadeAuditada")]
    public string? NomeEntidadeAuditada { get; set; }
    
    [BsonElement("Usuario")]
    public BsonDocument Usuario { get; set; }
    
    [BsonElement("Tipo")]
    [BsonRepresentation(BsonType.Int32)]
    public TipoLog TipoLog { get; set; }
    
    [BsonElement("Dados")]
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