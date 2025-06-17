using System.Reflection;
using Auditoria.Infra.DependencyInjection;
using Auditoria.Infra.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auditoria.Infra;

public static class InfraModule
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableTo<ISingletonDependency>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime()
            .AddClasses(classes => classes.AssignableTo<ITransientDependency>())
            .AsImplementedInterfaces()
            .WithTransientLifetime()
            .AddClasses(x => x.Where(y => y is { IsPublic: true, IsAbstract: false } && y.GetInterfaces().Any(z => z.Namespace == null || !z.Namespace.StartsWith("System") && z.Name != "IHostedService")))
            .AsImplementedInterfaces()
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
        
        services.Configure<RabbitMqConfig>(configuration.GetSection("RabbitMq"));
        
        return services;
    }
    
}