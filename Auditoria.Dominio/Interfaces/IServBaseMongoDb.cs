namespace Auditoria.Dominio.Interfaces;

public interface IServBaseMongoDb<T>
{
    Task<T> Inserir(T entidade);
}