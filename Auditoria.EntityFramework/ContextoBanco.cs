using System.Reflection;
using Auditoria.Dominio.Entidades;
using Auditoria.Infra.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;

namespace Auditoria.EntityFramework;

public class ContextoBanco : DbContext
{
    private readonly IAmbienteHelper _ambienteHelper;
    
    public ContextoBanco(DbContextOptions<ContextoBanco> contexto, 
                         IAmbienteHelper ambienteHelper)
        : base(contexto)
    {
        _ambienteHelper = ambienteHelper;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        foreach (var type in GetEntityTypesTenant(typeof(IdentificadorTenant)))
        {
            var method = SetGlobalQueryIdentificadorTenantMethod.MakeGenericMethod(type);
            method.Invoke(this, new object[] { modelBuilder });
        }

        foreach (var type in GetEntityTypesUnidade(typeof(IdentificadorUnidade)))
        {
            var method = SetGlobalQueryIdentificadorUnidadeMethod.MakeGenericMethod(type);
            method.Invoke(this, new object[] { modelBuilder });
        }
    }
    
    private static IList<Type>? _entityTypeCacheTenant;
    private static IList<Type> GetEntityTypesTenant(Type type)
    {
        if (_entityTypeCacheTenant != null)
        {
            return _entityTypeCacheTenant.ToList();
        }

        _entityTypeCacheTenant = GetEntityTypes(type);

        return _entityTypeCacheTenant;
    }
    
    private static IList<Type>? _entityTypeCacheUnidade;
    private static IList<Type> GetEntityTypesUnidade(Type type)
    {
        if (_entityTypeCacheUnidade != null)
        {
            return _entityTypeCacheUnidade.ToList();
        }

        _entityTypeCacheUnidade = GetEntityTypes(type);

        return _entityTypeCacheUnidade;
    }
    
    private static List<Type> GetEntityTypes(Type type)
    {
        return (from a in GetReferencingAssemblies()
            from t in a.DefinedTypes
            where t.BaseType == type
            select t.AsType()).ToList();
    }

    private static List<Assembly> GetReferencingAssemblies()
    {
        var assemblies = new List<Assembly>();
        //var dependencies = DependencyContext.Default?.RuntimeLibraries?.Where(x => x.Name.StartsWith("SistemaAcademia."));
        var dependencies = DependencyContext.Default.RuntimeLibraries;
        
        if (dependencies == null)
            return assemblies;

        foreach (var library in dependencies)
        {
            try
            {
                var assembly = Assembly.Load(new AssemblyName(library.Name));
                assemblies.Add(assembly);
            }
            catch (FileNotFoundException)
            { }
        }
        return assemblies;
    }
    
    private static readonly MethodInfo SetGlobalQueryIdentificadorTenantMethod = typeof(ContextoBanco)
        .GetMethods(BindingFlags.Public | BindingFlags.Instance)
        .Single(t => t is { IsGenericMethod: true, Name: "SetGlobalQueryIdentificadorTenant" });
    public void SetGlobalQueryIdentificadorTenant<T>(ModelBuilder builder) where T : IdentificadorTenant
    {
        builder.Entity<T>().HasQueryFilter(x => x.CodigoTenant == _ambienteHelper.RecuperarCodigoTenant() || _ambienteHelper.Autenticado() == false);
    }

    private static readonly MethodInfo SetGlobalQueryIdentificadorUnidadeMethod = typeof(ContextoBanco)
        .GetMethods(BindingFlags.Public | BindingFlags.Instance)
        .Single(t => t is { IsGenericMethod: true, Name: "SetGlobalQueryIdentificadorUnidade" });
    public void SetGlobalQueryIdentificadorUnidade<T>(ModelBuilder builder) where T : IdentificadorUnidade
    {
        builder.Entity<T>().HasQueryFilter(x => x.CodigoUnidade == _ambienteHelper.RecuperarCodigoUnidade() || _ambienteHelper.Autenticado() == false);
    }
    
    
}