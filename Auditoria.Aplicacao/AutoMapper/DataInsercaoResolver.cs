using Auditoria.Dominio.LogsAuditoria;
using Auditoria.Dominio.Views;
using AutoMapper;

public class DataInsercaoResolver : IValueResolver<LogAuditoria, LogAuditoriaView, DateTime>
{
    public DateTime Resolve(LogAuditoria src, LogAuditoriaView dest, DateTime destMember, ResolutionContext context)
    {
        if (src.DataInsercao?.DataCriacao == null)
        {
            return DateTime.MinValue;
        }

        try
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(src.DataInsercao.CodigoFusoHorario);
            return TimeZoneInfo.ConvertTimeFromUtc(src.DataInsercao.DataCriacao, timeZone);
        }
        catch
        {
            return src.DataInsercao.DataCriacao;
        }
    }
}