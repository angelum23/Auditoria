using MongoDB.Bson;

namespace Auditoria.Dominio.Entidades;

public interface IEntidadeMongoDb
{
    ObjectId Id { get; set; }
    DataInsercao? DataInsercao { get; set; }
}