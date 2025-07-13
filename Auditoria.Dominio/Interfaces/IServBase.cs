namespace Auditoria.Dominio.Interfaces;

public interface IServBase<TEntidade, TView>
{
    Task<TEntidade> Inserir(TEntidade entidade);
    Task<List<TView>> Recuperar(IPagedRequest paginacao);
}