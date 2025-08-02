using Auditoria.Dominio.Entidades;
using Auditoria.Dominio.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Auditoria.Mongo.Base.Repositorios;

public abstract class RepBaseMongoDb<TEntidade>(IMongoDatabase mongoDatabase) : IRepBaseMongoDb<TEntidade>
    where TEntidade : IdentificadorMongoDb
{
    private readonly IMongoCollection<TEntidade> _collection = mongoDatabase.GetCollection<TEntidade>(typeof(TEntidade).Name);

    public virtual async Task<TEntidade> CreateAsync(TEntidade model)
    {
        var idGerado = ObjectId.GenerateNewId();

        model.Id = idGerado;
        model.DataInsercao = new DataInsercao
        {
            DataCriacao = idGerado.CreationTime.ToUniversalTime(),
            CodigoFusoHorario = TimeZoneInfo.Local.Id
        };
        
        await _collection.InsertOneAsync(model);
        return model;
    }
    
    public virtual async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(c => c.Id.ToString() == id);
    }

    public virtual async Task<List<TEntidade>> GetAllAsync(IPagedRequest paginacao)
    {
        var query = _collection
            .Find(_ => true)
            .Skip(paginacao.Skip)
            .Limit(paginacao.Take);
        
        return await query.ToListAsync();
    }

    public virtual async Task<TEntidade> GetByIdAsync(string id)
    {
        return await _collection.Find(model => model.Id.ToString() == id).FirstOrDefaultAsync();
    }
}