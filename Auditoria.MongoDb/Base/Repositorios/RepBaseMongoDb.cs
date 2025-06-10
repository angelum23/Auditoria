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
        model.Id = ObjectId.GenerateNewId();
        model.DataCriacao = model.Id.CreationTime;
        await _collection.InsertOneAsync(model);
        return model;
    }
    
    public virtual async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(c => c.Id.ToString() == id);
    }

    public virtual async Task<IEnumerable<TEntidade>> GetAllAsync(int offset, int fetch)
    {
        var filter = Builders<TEntidade>.Filter.Empty;

        return await _collection
            .Find(filter)
            .Skip(offset)
            .Limit(fetch)
            .ToListAsync();
    }

    public virtual async Task<TEntidade> GetByIdAsync(string id)
    {
        return await _collection.Find(model => model.Id.ToString() == id).FirstOrDefaultAsync();
    }
}