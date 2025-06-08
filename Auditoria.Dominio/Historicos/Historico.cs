using System.Net;
using Auditoria.Dominio.Entidades;

namespace Auditoria.Dominio.Historicos;

public class Historico : IdentificadorMongoDb
{
    public DateTime DataHora { get; set; }
    public required string ChaveApi { get; set; }
    public int CodigoTenant { get; set; }
    public int CodigoUnidade { get; set; }
    public string? Operacao { get; set; }
    public string? Url { get; set; }
    public HttpStatusCode SituacaoRetorno { get; set; }
}