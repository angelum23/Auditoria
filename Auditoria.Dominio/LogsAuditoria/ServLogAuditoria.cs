using Auditoria.Dominio.Base;
using Auditoria.Dominio.Interfaces;

namespace Auditoria.Dominio.LogsAuditoria;

public class ServLogAuditoria(IRepLogAuditoria rep) : ServBaseMongoDb<LogAuditoria>(rep), IServLogAuditoria;