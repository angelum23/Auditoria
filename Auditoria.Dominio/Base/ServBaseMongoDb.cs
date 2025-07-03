using Auditoria.Dominio.Entidades;
using Auditoria.Dominio.Interfaces;
using MongoDB.Bson;

namespace Auditoria.Dominio.Base;

public class ServBaseMongoDb<T>(IRepBaseMongoDb<T> rep) : IServBase<T> where T : IdentificadorMongoDb
{
    public async Task<T> Inserir(T entidade)
    {
        await rep.CreateAsync(entidade);
        return entidade;
    }

    public async Task<List<T>> Recuperar(IPagedRequest paginacao)
    {
        return await rep.GetAllAsync(paginacao);
    }
}