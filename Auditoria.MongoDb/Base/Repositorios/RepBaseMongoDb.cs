using Auditoria.Dominio.Entidades;
using Auditoria.Dominio.Interfaces;
using MongoDB.Driver;

namespace Auditoria.Mongo.Base.Repositorios;

public class RepBaseMongoDb<TEntidade> : IRepBaseMongoDb<TEntidade>
    where TEntidade : IdentificadorMongoDb
{
    private readonly IMongoCollection<TEntidade> _collection;
    
    public RepBaseMongoDb(IMongoDatabase mongoDatabase)
    {
        _collection = mongoDatabase.GetCollection<TEntidade>(typeof(TEntidade).Name);
    }
    
    public async Task<TEntidade> CreateAsync(TEntidade model)
    {
        model.Id = Guid.NewGuid().ToString();
        await _collection.InsertOneAsync(model);
        return model;
    }
    
    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<TEntidade>> GetAllAsync(int offset, int fetch)
    {
        var filter = Builders<TEntidade>.Filter.Empty;

        return await _collection
            .Find(filter)
            .Skip(offset)
            .Limit(fetch)
            .ToListAsync();
    }

    public async Task<TEntidade> GetByIdAsync(string id)
    {
        return await _collection.Find(model => model.Id == id).FirstOrDefaultAsync();
    }
}