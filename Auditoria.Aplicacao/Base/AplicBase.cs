using Auditoria.Dominio.Interfaces;

namespace Auditoria.Aplicacao.Base;

public class AplicBase<T>(IServBase<T> serv) : IAplicBase<T>
{
    public Task<List<T>> RecuperarAsync(IPagedRequest paginacao)
    {
        return serv.Recuperar(paginacao);
    }
}