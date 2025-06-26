using Auditoria.Dominio.Base;
using Auditoria.Dominio.Interfaces;

namespace Auditoria.Dominio.Auditaveis;

public class ServLogAuditoria(IRepLogAuditoria rep) : ServBaseMongoDb<LogAuditoria>(rep), IServLogAuditoria
{
}