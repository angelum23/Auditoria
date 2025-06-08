using System.ComponentModel;

namespace Auditoria.Infra.Shared;

public enum EnumFusoHorario
{
    [Description("Brasília/São Paulo")] //GMT-3
    Brasilia = 1,

    [Description("Manaus")] //GMT-4
    Manaus = 2,

    [Description("Fernando de Noronha")] //GMT-2
    Noronha = 3,

    [Description("Rio Branco")] //GMT-5
    RioBranco = 4,

    [Description("Nordeste")] //GMT-3
    Nordeste = 5,

    [Description("Cuiabá")] //GMT-4
    Cuiaba = 6,

    [Description("Lisboa - Londres - Dublin")] //GMT+1
    GreenwichMeanTime = 7,

    [Description("Eastern Time (US & Canada)")] //GMT-5
    EasternTimeUsCanada = 8
}