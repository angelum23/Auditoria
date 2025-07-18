﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Auditoria.Dominio.Interfaces;

namespace Auditoria.Dominio.Infra;

public static class DominioModule
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(x => x.Where(y => y is { IsPublic: true, IsAbstract: false } && y.GetInterfaces().Any(z => z.Namespace == null || !z.Namespace.StartsWith("System"))))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
            
        return services;
    }
}