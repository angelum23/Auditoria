using Auditoria.Dominio.LogsAuditoria;
using MongoDB.Bson;

namespace Auditoria.Dominio.Views;

public record LogAuditoriaView
{
    public ObjectId Id { get; set; }
    public int CodigoTenant { get; set; }
    public int CodigoUnidade { get; set; }
    public string? EntidadeAuditada { get; set; }
    public Dictionary<string, string> Usuario { get; set; }
    public TipoLog TipoLog { get; set; }
    public Dictionary<string, string> Dados { get; set; }
}