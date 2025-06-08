using System.Reflection;
using Auditoria.Infra.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Auditoria.Mongo;

public static class MongoDbModule
{
    public static IServiceCollection AddMongoDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Mongo");
        var url = new MongoUrl(connectionString);
        var mongoClient = new MongoClient(url);
        var mongoDatabase = mongoClient.GetDatabase(url.DatabaseName);
        
        services.AddScoped(provider => mongoDatabase);
        
        var assembly = Assembly.GetExecutingAssembly();
        
        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableTo<ISingletonDependency>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime()
            .AddClasses(x => x.Where(y => y.GetInterfaces().All(z => z.Name != "ISingletonDependency")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        return services;
    }
}