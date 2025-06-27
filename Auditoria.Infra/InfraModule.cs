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
        );
        
        services.AddSingleton<IRabbitMqConnectionManager, RabbitMqConnectionManager>();

        services.Configure<RabbitMqConfig>(configuration.GetSection("RabbitMq"));
        
        return services;
    }
    
}