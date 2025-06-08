using System.Security.Claims;
using Auditoria.Infra.Shared;

namespace Auditoria.Infra.Authentication;

public interface IAmbienteHelper
{
    int RecuperarCodigoTenant();
    int RecuperarCodigoUnidade();
    string? RecuperarUsuario();
    bool Autenticado();
    void LogarFixo(int codigoUsuario, string usuario, int codigoTenant, int codigoUnidadePreferencial, EnumFusoHorario fusoHorario);
    public ClaimsIdentity? Identity { get; set; }
}