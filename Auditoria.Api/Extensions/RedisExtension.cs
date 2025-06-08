using Serilog;
using StackExchange.Redis;
using ILogger = Serilog.ILogger;

namespace Auditoria.Api.Extensions;

public static class RedisExtension
{
    private static readonly ILogger Logger = Log.ForContext(typeof(RedisExtension));
    
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        try
        {
            Logger.Information("ENABLE_REDIS: {ENABLE_REDIS}", configuration.GetSection("ENABLE_REDIS").Value);
            if (configuration.GetSection("ENABLE_REDIS").Value != "false")
            {
                var hostRedis = configuration.GetConnectionString("Redis");
                var pass = configuration.GetSection("REDIS_PASSWORD").Value;

                var redisCs = hostRedis;
                if (!string.IsNullOrWhiteSpace(pass))
                    redisCs += $",password={pass}";

                redisCs += ",abortConnect=false,connectTimeout=2000,syncTimeout=800,asyncTimeout=800";

                Logger.Information($"redisCs: {redisCs}");

                var cm = ConnectionMultiplexer.Connect(redisCs);
                services.AddSingleton(cm);
            }

        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Erro ao adicionar Redis.");
        }
        
        return services;
    }
}