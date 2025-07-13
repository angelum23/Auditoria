using Auditoria.Aplicacao.Base.Requests;
using Auditoria.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Auditoria.Api.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
public class ControllerBaseLeitura<TEntidade, TView>(IAplicBase<TEntidade, TView> aplic) : ControllerBase
{
    [HttpGet]
    public virtual async Task<IActionResult> RecuperarAsync([FromQuery] PagedRequest paginacao)
    {
        try
        {
            var dados = await aplic.RecuperarAsync(paginacao);
            return Ok(dados);
        }
        catch (Exception e)
        {
            return Falha(e.Message);
        }
    }
}