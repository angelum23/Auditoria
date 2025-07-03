using Auditoria.Aplicacao.Base;
using Auditoria.Dominio.Interfaces;
using Auditoria.Dominio.LogsAuditoria;

namespace Auditoria.Aplicacao.LogsAuditoria;

public class AplicLogAuditoria(IServBase<LogAuditoria> serv) : AplicBase<LogAuditoria>(serv), IAplicLogAuditoria;