using Auditoria.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Auditoria.Api.Controllers.Base;

[ApiController]
[Route("[controller]")]
public class ControllerBaseLeitura<T>(IAplicBase<T> aplic) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> RecuperarAsync([FromQuery] IPagedRequest paginacao)
    {
        try
        {
            var dados = await aplic.RecuperarAsync(paginacao);
            return Sucesso(content: dados);
        }
        catch (Exception e)
        {
            return Falha(e.Message);
        }
    }
}