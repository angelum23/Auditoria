using System.ComponentModel;

namespace Auditoria.Dominio.Shared.ContasReceber;

public enum EnumStatusReceber
{
    [Description("Aberto")]
    Aberto = 1,
    [Description("Recebido")]
    Recebido = 2,
    [Description("Cancelado")]
    Cancelado = 3,
    [Description("Renegociado")]
    Renegociado = 4,
    [Description("Em andamento")]
    EmAndamento = 5
}