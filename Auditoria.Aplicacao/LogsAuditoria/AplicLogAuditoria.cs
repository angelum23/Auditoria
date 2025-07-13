using Auditoria.Aplicacao.Base;
using Auditoria.Dominio.Interfaces;
using Auditoria.Dominio.LogsAuditoria;
using Auditoria.Dominio.Views;

namespace Auditoria.Aplicacao.LogsAuditoria;

public class AplicLogAuditoria(IServBase<LogAuditoria, LogAuditoriaView> serv) : AplicBase<LogAuditoria, LogAuditoriaView>(serv), IAplicLogAuditoria;