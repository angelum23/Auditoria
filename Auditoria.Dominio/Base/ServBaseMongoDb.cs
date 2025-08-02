using System.Diagnostics;
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
        List<TView> resultados;
        
        try
        {
            resultados = mapper.Map<List<TView>>(registros);
        }
        catch (AutoMapperMappingException ex)
        {
            Debug.WriteLine($"Erro ao mapear: {ex.Message}");
            if (ex.InnerException != null)
            {
                Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
            }
            
            throw;
        }

        return resultados;
    }
}