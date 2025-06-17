using System.Reflection;
using Auditoria.Infra.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auditoria.Aplicacao;

public static class AplicacaoModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(x => x.Where(y => y is { IsPublic: true, IsAbstract: false } && y.GetInterfaces().Any(z => z.Namespace == null || !z.Namespace.StartsWith("System"))))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        services.AddHostedService<LogAuditoriaConsumer>();
        services.AddAutoMapper(assembly);
        return services;
    }
}