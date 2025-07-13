using Auditoria.Dominio.Interfaces;

namespace Auditoria.Aplicacao.Base;

public class AplicBase<TEntidade, TView>(IServBase<TEntidade, TView> serv) : IAplicBase<TEntidade, TView>
{
    public Task<List<TView>> RecuperarAsync(IPagedRequest paginacao)
    {
        return serv.Recuperar(paginacao);
    }
}