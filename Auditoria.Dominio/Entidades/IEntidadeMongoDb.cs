using MongoDB.Bson;

namespace Auditoria.Dominio.Entidades;

public interface IEntidadeMongoDb
{
    ObjectId Id { get; set; }
    DateTime DataCriacao { get; set; }
}