using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auditoria.Aplicacao;

public static class AplicacaoModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        services.Scan(scan => scan.FromAssemblies(assembly)
            //.AddClasses()
            .AddClasses(x => x.Where(y => y is { IsPublic: true, IsAbstract: false } && y.GetInterfaces().Any(z => z.Namespace == null || !z.Namespace.StartsWith("System"))))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        //var collection = new ServiceCollection();

        // collection.Scan(scan => scan
        //     // We start out with all types in the assembly of ITransientService
        //     .FromAssemblyOf<ITransientService>()
        //     // AddClasses starts out with all public, non-abstract types in this assembly.
        //     // These types are then filtered by the delegate passed to the method.
        //     // In this case, we filter out only the classes that are assignable to ITransientService.
        //     .AddClasses(classes => classes.AssignableTo<ITransientService>())
        //     // We then specify what type we want to register these classes as.
        //     // In this case, we want to register the types as all of its implemented interfaces.
        //     // So if a type implements 3 interfaces; A, B, C, we'd end up with three separate registrations.
        //     .AsImplementedInterfaces()
        //     // And lastly, we specify the lifetime of these registrations.
        //     .WithTransientLifetime()
        //     // Here we start again, with a new full set of classes from the assembly above.
        //     // This time, filtering out only the classes assignable to IScopedService.
        //     .AddClasses(classes => classes.AssignableTo<IScopedService>())
        //     // Now, we just want to register these types as a single interface, IScopedService.
        //     .As<IScopedService>()
        //     // And again, just specify the lifetime.
        //     .WithScopedLifetime()
        //     // Generic interfaces are also supported too, e.g. public interface IOpenGeneric<T> 
        //     .AddClasses(classes => classes.AssignableTo(typeof(IOpenGeneric<>)))
        //     .AsImplementedInterfaces()
        //     // And you scan generics with multiple type parameters too
        //     // e.g. public interface IQueryHandler<TQuery, TResult>
        //     .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
        //     .AsImplementedInterfaces());
        
        services.AddAutoMapper(assembly);
        return services;
    }
}