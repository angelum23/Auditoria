using Auditoria.Dominio.LogsAuditoria;
using MongoDB.Bson;

namespace Auditoria.Dominio.Views;

public record LogAuditoriaView
{
    public int CodigoOriginal { get; set; }
    public DadosInseridorView? DadosInseridor { get; set; }
    public DadosAcaoView? DadosAcao { get; set; }
    public Dictionary<string, string>? Dados { get; set; }
}

public record DadosInseridorView
{
    public int CodigoTenant { get; set; }
    public int CodigoUnidade { get; set; }
    public int CodigoUsuario { get; set; }
}

public record DadosAcaoView
{
    public string? EntidadeAuditada { get; set; }
    public TipoLog TipoLog { get; set; }
}