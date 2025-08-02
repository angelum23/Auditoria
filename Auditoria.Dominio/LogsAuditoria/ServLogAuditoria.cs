using Auditoria.Dominio.Interfaces;
using Auditoria.Dominio.Views;
using AutoMapper;

namespace Auditoria.Dominio.LogsAuditoria;

public class ServLogAuditoria(IRepLogAuditoria rep, IMapper mapper)
    : ServBaseMongoDb<LogAuditoria, LogAuditoriaView>(rep, mapper), IServLogAuditoria
{
    private readonly IMapper _mapper = mapper;

    public async Task Inserir(InserirLogAuditoriaDTO dto)
    {
        var logAuditoria = _mapper.Map<LogAuditoria>(dto);
        await base.Inserir(logAuditoria);
    }
}