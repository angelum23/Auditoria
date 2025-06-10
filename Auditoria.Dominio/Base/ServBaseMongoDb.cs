using Auditoria.Dominio.Entidades;
using Auditoria.Dominio.Interfaces;

namespace Auditoria.Dominio.Base;

public class ServBaseMongoDb<T>(IRepBaseMongoDb<T> rep) where T : IdentificadorMongoDb
{
    public async Task<T> Inserir(T entidade)
    {
        await rep.CreateAsync(entidade);
        return entidade;
    }
}