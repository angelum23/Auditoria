﻿using System.Reflection;
using Auditoria.Infra.RabbitMq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Auditoria.Aplicacao;

public static class AplicacaoModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(x => x.Where(y => y is { IsPublic: true, IsAbstract: false } 
                                          && y.BaseType != typeof(BackgroundService)
                                       && y.GetInterfaces().Any(z => z.Namespace == null || !z.Namespace.StartsWith("System"))))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        services.AddHostedService<LogAuditoriaConsumer>();
        return services;
    }
}