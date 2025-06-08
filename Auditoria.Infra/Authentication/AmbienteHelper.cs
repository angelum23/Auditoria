using System.Security.Claims;
using Auditoria.Infra.DependencyInjection;
using Auditoria.Infra.Shared;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Auditoria.Infra.Authentication;

public class AmbienteHelper : IAmbienteHelper, ITransientDependency
{
    private readonly IHttpContextAccessor _context;

    public AmbienteHelper(IHttpContextAccessor context)
    {
        _context = context;
    }

    #region Identity
    public ClaimsIdentity? Identity
    {
        get
        {
            try
            {
                if (_context?.HttpContext?.User is { Identity.IsAuthenticated: true })
                {
                    return _context.HttpContext.User.Identity as ClaimsIdentity;
                }
                return Thread.CurrentPrincipal != null ? Thread.CurrentPrincipal.Identity as ClaimsIdentity : null;
            }
            catch
            {
                return null;
            }
        }
        set
        {
            if (value is null)
            {
                return;
            }
            
            if (_context.HttpContext != null)
            {
                _context.HttpContext.User = new ClaimsPrincipal(value);
            }
            else
            {
                Thread.CurrentPrincipal = new ClaimsPrincipal(value);
            }
        }
    }
    #endregion
    public int RecuperarCodigoTenant()
    {
        if (!Autenticado() || Identity is null)
        {
            return 0;
        }
        
        return int.Parse(Identity.Claims.Where(x => x.Type == "codigoTenant").Select(x => x.Value).First());
    }

    public int RecuperarCodigoUnidade()
    {
        if (!Autenticado() || Identity is null)
        {
            return 0;
        }
        
        return int.Parse(Identity.Claims.Where(x => x.Type == "codigoUnidadePreferencial").Select(x => x.Value).First());
    }

    public string? RecuperarUsuario()
    {
        if (!Autenticado() || Identity is null)
        {
            return default;
        }
        
        return Identity.Claims.Where(x => x.Type == "usuario").Select(x => x.Value).First();
    }
    
    public bool Autenticado()
    {
        if (Identity == null)
            return false;

        return (bool)Identity?.Claims.Any();
    }
    
    #region LogarFixo
    
    public void LogarFixo(int codigoUsuario, string usuario, int codigoTenant, int codigoUnidadePreferencial, EnumFusoHorario fusoHorario)
    {
        Identity = new ClaimsIdentity(new List<Claim>
        {
            new Claim("codigoUsuario", codigoUsuario.ToString()),
            new Claim("usuario", usuario),
            new Claim("codigoTenant", codigoTenant.ToString()),
            new Claim("codigoUnidadePreferencial", codigoUnidadePreferencial.ToString()),
            new Claim("fusoHorario", ((int) fusoHorario).ToString()),
            new Claim("senhaGlobal", true.ToString()),
            new Claim("bloqueado", false.ToString()),
            new Claim("usuarioBot", false.ToString()),
            new Claim("unidades", JsonConvert.SerializeObject(new List<DadosAmbienteUnidadeDTO>{new() { CodigoUnidade = codigoUnidadePreferencial, FusoHorario = fusoHorario }}))
        }, "Local");
    }
    #endregion
}