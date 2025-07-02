using Auditoria.Dominio.Auditaveis;
using MongoDB.Bson;

namespace Auditoria.Dominio.Views;

public class LogAuditoriaView
{
    public string? NomeEntidadeAuditada { get; set; }
    public BsonDocument Usuario { get; set; }
    public TipoLog TipoLog { get; set; }
    public BsonDocument Dados { get; set; }
}