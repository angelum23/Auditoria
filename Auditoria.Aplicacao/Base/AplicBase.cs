using Auditoria.Aplicacao.Base.Requests;
using Auditoria.Dominio.Entidades;
using Auditoria.Dominio.Interfaces;
using Auditoria.Infra.Threading;
using AutoMapper;

namespace Auditoria.Aplicacao.Base;

public abstract class AplicBase<TEntidade, TView> : ApplicationService, IAplicBase<TEntidade, TView>
    where TEntidade : class, IEntidade
{
    protected AplicBase(IRepBase<TEntidade> repositorio, IMapper mapper, IAsyncQueryableExecuter asyncExecuter)
    {
        Repositorio = repositorio;
        Mapper = mapper;
        AsyncExecuter = asyncExecuter;
    }

    protected IRepBase<TEntidade> Repositorio { get; }
    protected IMapper Mapper { get; }
    protected IAsyncQueryableExecuter AsyncExecuter { get; }
    
    
    
    protected virtual async Task<List<TView>> MapearParaListaDeDtoAsync(List<TEntidade> entities)
    {
        var dtos = new List<TView>();

        foreach (var entity in entities)
        {
            dtos.Add(await MapearParaViewAsync(entity));
        }

        return dtos;
    }
    
    protected virtual Task<TView> MapearParaViewAsync(TEntidade entity)
    {
        return Task.FromResult(MapearParaView(entity));
    }
    
    protected virtual TView MapearParaView(TEntidade entity)
    {
        return Mapper.Map<TEntidade, TView>(entity);
    }
    
    protected virtual IQueryable<TEntidade> AplicarOrdenacaoPadrao(IQueryable<TEntidade> query)
    {
        if (typeof(TEntidade).IsAssignableTo(typeof(IdentificadorMongoDb)))
        {
            return query.OrderByDescending(e => ((IEntidadeMongoDb)e).Id);
        }

        throw new Exception("Ordenação obrigatória");
    }
    
    protected virtual IQueryable<TEntidade> AplicarPaginacao(IQueryable<TEntidade> query, PagedRequest paginacao)
    {
        return query
            .Skip(paginacao.Skip)
            .Take(paginacao.Take);
    }
    
    protected virtual IQueryable<TEntidade> RecuperarQueryOrdenada(bool asNoTracking = false)
    {
        return AplicarOrdenacaoPadrao(Repositorio.Recuperar(asNoTracking));
    }
}