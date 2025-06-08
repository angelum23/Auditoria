using Auditoria.Dominio.Entidades;
using Auditoria.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Auditoria.EntityFramework.Base.Repositorios;

public class RepBase<TEntidade> : IRepBase<TEntidade>
    where TEntidade : class, IEntidade
{
    private readonly ContextoBancoLeitura _dbContext;

    public RepBase(ContextoBancoLeitura dbContext)
    {
        _dbContext = dbContext;
    }

    protected virtual DbSet<TEntidade> DbSet => _dbContext.Set<TEntidade>();
    
    public IQueryable<TEntidade> Recuperar(bool asNoTracking = false, params string[] includes)
    {
        var query = DbSet.AsQueryable();

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        return includes.Length == 0 ? query : includes.Aggregate(query, (current, include) => current.Include(include));
    }
    
    public IQueryable<TEntidade> Recuperar(params string[] includes)
    {
        return includes.Length == 0 ? DbSet : includes.Aggregate(DbSet.AsQueryable(), (current, include) => current.Include(include));
    }
}