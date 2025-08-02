using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Auditoria.Dominio.Entidades;

public class IdentificadorMongoDb : IEntidadeMongoDb
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required ObjectId Id { get; set; }
    
    [BsonElement("datacriacao")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime DataCriacao { get; set; }
    
    [BsonElement("codigofusohorario")]
    [BsonRepresentation(BsonType.String)]
    public string CodigoFusoHorario { get; set; } = string.Empty;
}