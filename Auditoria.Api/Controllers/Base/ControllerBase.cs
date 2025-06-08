using System.Net;
using Auditoria.Aplicacao.Historicos;
using Auditoria.Aplicacao.Results;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Auditoria.Api.Controllers.Base;

public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
    protected IAplicHistorico AplicHistorico { get; }
    public ControllerBase(IAplicHistorico aplicHistorico)
    {
        AplicHistorico = aplicHistorico;
    }

    //[NonAction]
    private IActionResult CustomResult<TResposta>(HttpStatusCode statusCode, TResposta resposta)
    {
        var response = new ObjectResult(resposta)
        {
            StatusCode = (int) statusCode
        };
        
        return response;
    }
    
    protected IActionResult Falha(HttpErrorMessage resposta)
    {
        return CustomResult(HttpStatusCode.BadRequest, resposta);
    }
    protected IActionResult Falha(string mensagem = "", object content = null, Enum errorCode = null)
    {
        return Falha(new HttpErrorMessage
        {
            Message = mensagem,
            Content = content,
            ErrorCode = errorCode == null 
                ? 0// (int) EnumErro.ErroDesconhecido 
                : Convert.ToInt32(errorCode)
        });
    }

    [NonAction]
    protected IActionResult Falha(Exception exp)
    {
        Enum errorCode = null;
        // if (exp is AppException appException)
        // {
        //     errorCode = appException.CodigoErro;
        //     //new Thread(threadInfo =>
        //     //{
        //     //    if (Convert.ToInt32(errorCode) > 1
        //     //     || TelemetryConfiguration.Active.DisableTelemetry)
        //     //    {
        //     //        return;
        //     //    }
        //     //    var telemetryClient = new TelemetryClient();
        //     //    telemetryClient.TrackException(exp);
        //     //    telemetryClient.Flush();
        //     //}).Start();
        // }

        return Falha(exp.Message, null, errorCode);
    }
    
    protected async Task<IActionResult> Falha(Erro erro)
    {
        await AplicHistorico.Inserir(Request, HttpStatusCode.BadRequest); 
        return Falha(erro.Descricao, null, HttpStatusCode.BadRequest);
    }
    
    
    protected IActionResult Sucesso(HttpSuccessMessage resposta)
    {
        return CustomResult(HttpStatusCode.OK, resposta);
    }

    protected async Task<IActionResult> Sucesso([ActionResultObjectValue] object? value)
    {
        await AplicHistorico.Inserir(Request, HttpStatusCode.OK);
        return CustomResult(HttpStatusCode.OK, value);
    }


    protected IActionResult Sucesso(string mensagem = "", object content = null, int? codeRequest = null)
    {
        return Sucesso(new HttpSuccessMessage
        {
            Message = mensagem,
            Content = content ?? new { }
        });
    }
    

    [NonAction]
    protected IActionResult Lista(HttpListMessage resposta)
    {
        return CustomResult(HttpStatusCode.OK, resposta);
    }
}

public class HttpSuccessMessage : HttpBaseMessage
{
    public HttpSuccessMessage()
    {
        Success = true;
    }
}

public class HttpBaseMessage<T>
{
    public T Content { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
}

public class HttpBaseMessage : HttpBaseMessage<object>
{
}
public class HttpErrorMessage : HttpBaseMessage
{
    #region ctor
    public int ErrorCode { get; set; }

    public HttpErrorMessage()
    {
        Success = false;
        ErrorCode = -1;
    }
    #endregion
}

public class HttpListMessage : HttpSuccessMessage
{
    public int Total { get; set; }
    public bool First { get; set; }
    public bool Last { get; set; }
}