using Mongo2Go;
using MongoDB.Driver;

namespace Auditoria.MongoDbTest.Base;

public abstract class RepTestBase : IDisposable
{
    private readonly MongoDbRunner _runner;
    protected readonly IMongoDatabase _database;

    protected RepTestBase()
    {
        _runner = MongoDbRunner.Start();
        var client = new MongoClient(_runner.ConnectionString);
        _database = client.GetDatabase("TestesDeIntegracao");
    }

    public void Dispose()
    {
        _runner.Dispose();
        GC.SuppressFinalize(this);
    }
}