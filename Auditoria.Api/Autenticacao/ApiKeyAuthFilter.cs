using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Auditoria.Api.Autenticacao;

[Obsolete("Substituído pelo ApiKeyAuthenticationHandler que é o padrão .NET 8")]
public class ApiKeyAuthFilter: IAsyncAuthorizationFilter
{
    private readonly IConfiguration _configuration;

    public ApiKeyAuthFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(AuthConsts.ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult("Api Key não informada");
            return Task.CompletedTask;
        }

        var apiKey = _configuration.GetValue<string>(AuthConsts.ApiKeySectionName);
        if (!string.IsNullOrWhiteSpace(apiKey) && apiKey.Equals(extractedApiKey))
        {
            return Task.CompletedTask;
        }
        
        context.Result = new UnauthorizedObjectResult("Api Key inválida");
        return Task.CompletedTask;
    }
}