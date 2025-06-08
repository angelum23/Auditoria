using System.Reflection;
using Auditoria.Infra.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Auditoria.EntityFramework;

public static class EntityFrameworkModule
{
    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var stringConexao = configuration.GetConnectionString("Conexao");
        if (string.IsNullOrEmpty(stringConexao))
        {
            throw new Exception("Chave [StringConexao] não foi encontrada ou string de conexão não foi informada.");
        }
        
        var stringConexaoReplica = configuration.GetConnectionString("ConexaoReplica");
        if (string.IsNullOrEmpty(stringConexaoReplica))
        {
            stringConexaoReplica = stringConexao;
        }

        services.AddDbContext<ContextoBancoLeitura>(o =>
        {
            o.UseNpgsql(new NpgsqlConnection(stringConexaoReplica), pgOptions =>
            {
                pgOptions.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
            });
#if DEBUG 
            o.EnableSensitiveDataLogging();
#endif
        });
        
        services.AddDbContext<ContextoBanco>(o =>
        {
            o.UseNpgsql(new NpgsqlConnection(stringConexao));
#if DEBUG 
            o.EnableSensitiveDataLogging();
#endif
        });
        
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