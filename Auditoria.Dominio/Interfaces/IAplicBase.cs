namespace Auditoria.Dominio.Interfaces;

public interface IAplicBase<TEntidade, TView>
{
    public Task<List<TView>> RecuperarAsync(IPagedRequest paginacao);
}