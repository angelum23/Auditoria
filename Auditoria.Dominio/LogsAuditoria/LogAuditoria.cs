using System.ComponentModel;
using Auditoria.Dominio.Entidades;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Auditoria.Dominio.LogsAuditoria;

/// <summary>
/// Entidade que representa um log de auditoria.
/// Contém informações de identificação, dados de quem inseriu, que ação foi feita e os dados desserializados.
/// </summary>
public class LogAuditoria : IdentificadorMongoDb
{
    /// <summary>
    /// Código original do registro auditado.
    /// </summary>
    [BsonElement("idoriginal")]
    public int CodigoOriginal { get; set; }
    
    /// <summary>
    /// Dados sobre quem inseriu o registro.
    /// </summary>
    [BsonElement("dadosInseridor")]
    public DadosInseridor? DadosInseridor { get; set; }
    
    /// <summary>
    /// Dados sobre a ação realizada.
    /// </summary>
    [BsonElement("dadosAcao")]
    public DadosAcao? DadosAcao { get; set; }
    
    /// <summary>
    /// Objeto completo da entidade manipulada, contendo todos os campos e informações recebidas.
    /// </summary>
    [BsonElement("dados")]
    public BsonDocument? DadoDesserializado { get; set; }
}

/// <summary>
/// Representa informações do usuário/serviço que realizou a inserção do registro.
/// </summary>
public class DadosInseridor
{
    /// <summary>
    /// Código do tenant associado à inserção.
    /// </summary>
    [BsonElement("idtenant")]
    public int CodigoTenant { get; set; }
    
    /// <summary>
    /// Código da unidade associada à inserção.
    /// </summary>
    [BsonElement("idunidade")]
    public int CodigoUnidade { get; set; }
    
    /// <summary>
    /// Código do usuário responsável pela inserção.
    /// </summary>
    [BsonElement("idusuario")]
    public int CodigoUsuario { get; set; }
}

/// <summary>
/// Representa informações sobre a ação auditada (entidade e tipo de log).
/// </summary>
public class DadosAcao
{
    /// <summary>
    /// Nome da classe/entidade auditada.
    /// </summary>
    [BsonElement("entidadeauditada")]
    public string? EntidadeAuditada { get; set; }
    
    /// <summary>
    /// Tipo da ação realizada (inserção, alteração, remoção).
    /// </summary>
    [BsonElement("tipo")]
    [BsonRepresentation(BsonType.String)]
    public TipoLog TipoLog { get; set; }
}

/// <summary>
/// Enumera os tipos de log de auditoria possíveis.
/// </summary>
public enum TipoLog
{
    [Description("Não recebido")]
    NaoRecebido = 0,
    
    [Description("Inserção de Entidade")]
    Insercao = 1,

    [Description("Alteração de Entidade")]
    Alteracao = 2,

    [Description("Remoção de Entidade")]
    Remocao = 3,
}