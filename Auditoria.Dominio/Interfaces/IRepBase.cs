using Auditoria.Dominio.Entidades;

namespace Auditoria.Dominio.Interfaces;

public interface IRepBase<TEntidade> : IRepository
    where TEntidade : class, IEntidade
{
    IQueryable<TEntidade> Recuperar(bool asNoTracking = false, params string[] includes);
    IQueryable<TEntidade> Recuperar(params string[] includes);
}