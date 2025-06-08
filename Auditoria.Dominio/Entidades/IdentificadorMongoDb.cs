namespace Auditoria.Dominio.Entidades;

public class IdentificadorMongoDb : ITemIdMongoDb
{
    public required string Id { get; set; }
}