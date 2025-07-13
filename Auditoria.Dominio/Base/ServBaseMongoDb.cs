using Auditoria.Dominio.Entidades;
using Auditoria.Dominio.Interfaces;
using AutoMapper;

public class ServBaseMongoDb<TEntidade, TView>(IRepBaseMongoDb<TEntidade> rep, IMapper mapper)
    : IServBase<TEntidade, TView>
    where TEntidade : IdentificadorMongoDb
{
    public async Task<TEntidade> Inserir(TEntidade entidade)
    {
        await rep.CreateAsync(entidade);
        return entidade;
    }

    public async Task<List<TView>> Recuperar(IPagedRequest paginacao)
    {
        var registros = await rep.GetAllAsync(paginacao);
        return mapper.Map<List<TView>>(registros);
    }
}