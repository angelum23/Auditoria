using Auditoria.Dominio.Interfaces;
using Auditoria.Dominio.Views;
using AutoMapper;

namespace Auditoria.Dominio.LogsAuditoria;

public class ServLogAuditoria(IRepLogAuditoria rep, IMapper mapper) : ServBaseMongoDb<LogAuditoria, LogAuditoriaView>(rep, mapper), IServLogAuditoria;