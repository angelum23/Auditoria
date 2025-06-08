using System.Net;
using Auditoria.Aplicacao.Base;
using Microsoft.AspNetCore.Http;

namespace Auditoria.Aplicacao.Historicos;

public interface IAplicHistorico : IApplicationService
{
    Task Inserir(HttpRequest request, HttpStatusCode statusCode);
}