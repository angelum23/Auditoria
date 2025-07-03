namespace Auditoria.Dominio.Interfaces;

public interface IAplicBase<T>
{
    public Task<List<T>> RecuperarAsync(IPagedRequest paginacao);
}