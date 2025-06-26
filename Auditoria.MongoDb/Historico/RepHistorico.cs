using Auditoria.Mongo.Base.Repositorios;
using MongoDB.Driver;

namespace Auditoria.MongoDb.Historico;

public class RepHistorico (IMongoDatabase mongoDatabase) 
    : RepBaseMongoDb<Dominio.Historicos.Historico>(mongoDatabase);