﻿using Auditoria.Dominio.Entidades;

namespace Auditoria.Dominio.Interfaces;

public interface IRepBaseMongoDb<TEntidade> : IRepository
    where TEntidade : IdentificadorMongoDb
{
    Task<TEntidade> CreateAsync(TEntidade reg);
    Task DeleteAsync(string id);
    Task<TEntidade> GetByIdAsync(string id);
    Task<List<TEntidade>> GetAllAsync(IPagedRequest paginacao);
}