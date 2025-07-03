using Auditoria.Dominio.Interfaces;
using Auditoria.Dominio.LogsAuditoria;
using Auditoria.Mongo.Base.Repositorios;
using MongoDB.Driver;

namespace Auditoria.Mongo.LogsAuditoria;

public class RepLogAuditoria(IMongoDatabase mongoDatabase) : RepBaseMongoDb<LogAuditoria>(mongoDatabase), IRepLogAuditoria;