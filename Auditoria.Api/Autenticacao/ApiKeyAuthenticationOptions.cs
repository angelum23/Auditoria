using Microsoft.AspNetCore.Authentication;

namespace Auditoria.Api.Autenticacao;

public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
{
    public const string DefaultScheme = AuthConsts.ApiKeyDefaultScheme;
    public const string HeaderName = AuthConsts.ApiKeyHeaderName;
}