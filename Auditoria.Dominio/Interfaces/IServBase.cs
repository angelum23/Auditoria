namespace Auditoria.Dominio.Interfaces;

public interface IServBase<T>
{
    Task<T> Inserir(T entidade);
    Task<List<T>> Recuperar(IPagedRequest paginacao);
}