using Auditoria.Api.Controllers.Base;
using Auditoria.Dominio.Interfaces;
using Auditoria.Dominio.LogsAuditoria;
using Auditoria.Dominio.Views;

namespace Auditoria.Api.Controllers;

public class LogAuditoriaController(IAplicLogAuditoria aplic) : ControllerBaseLeitura<LogAuditoria, LogAuditoriaView>(aplic);