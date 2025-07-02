using Auditoria.Aplicacao.Base.Requests;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = Auditoria.Api.Controllers.Base.ControllerBase;

namespace Auditoria.Api.Controllers;

public class LogAuditoriaController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;
    LogAuditoriaController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    [HttpGet]
    public async Task<IActionResult> Recuperar(PagedRequest paginacaoDto)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope(); //todo terminar
            return Sucesso();
        }
        catch (Exception e)
        {
            return Falha(e.Message);
        }
    }
}