using System.Diagnostics;
using Auditoria.Infra.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Auditoria.Infra.Caching;

public class NextSingletonCache : INextSingletonCache, ISingletonDependency
{
    private static ConnectionMultiplexer? _connectionMultiplexer;
    private static IDatabase? _redisDatabase;
    
    private readonly IConfiguration _configuration;
    private readonly ILogger<NextSingletonCache> _logger;
    private readonly IServiceProvider _serviceProvider;

    public NextSingletonCache(IConfiguration configuration,
        ILogger<NextSingletonCache> logger,
        IServiceProvider serviceProvider)
    {
        _configuration = configuration;
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected ConnectionMultiplexer GetConnectionMultiplexer()
    {
        if (_connectionMultiplexer != null)
            return _connectionMultiplexer;

        _connectionMultiplexer = _serviceProvider.GetRequiredService<ConnectionMultiplexer>();
        return _connectionMultiplexer;
    }

    protected IDatabase GetRedisDatabase()
    {
        if (_redisDatabase != null)
            return _redisDatabase;

        _redisDatabase = GetConnectionMultiplexer().GetDatabase();
        return _redisDatabase;
    }

    #region Set

    public async Task SetAsync<T>(string key, T value, int? expirationInSeconds = NextCacheHelper.DefaultExpirationInSeconds)
    {
        try
        {
            if (_configuration.GetSection("ENABLE_REDIS").Value == "false")
            {
                return;
            }

            var muxer = GetConnectionMultiplexer();
            if (!muxer.IsConnected)
            {
                return;
            }

            var expiry = expirationInSeconds.HasValue
                ? TimeSpan.FromSeconds(expirationInSeconds.Value)
                : (TimeSpan?)null;
            await GetRedisDatabase().StringSetAsync(key, JsonConvert.SerializeObject(value,
                new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto
                }), expiry);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro no SetAsync do cache. - {key} - {ex}", ex);
            if (Debugger.IsAttached)
                throw;
        }
    }


    public void Set<T>(string key, T value, int? expirationInSeconds = NextCacheHelper.DefaultExpirationInSeconds)
    {
        try
        {
            if (_configuration.GetSection("ENABLE_REDIS").Value == "false")
            {
                return;
            }

            var muxer = GetConnectionMultiplexer();
            if (!muxer.IsConnected)
            {
                return;
            }

            var expiry = expirationInSeconds.HasValue ? TimeSpan.FromSeconds(expirationInSeconds.Value) : (TimeSpan?)null;
            GetRedisDatabase().StringSet(key, JsonConvert.SerializeObject(value), expiry);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro no Set do cache. - {key} - {ex}", ex);
            if (Debugger.IsAttached)
                throw;
        }
    }

    #endregion

    #region Get

    public async Task<List<object>> GetAllAsync<T>(string pattern)
    {
        var list = new List<object>();
        try
        {
            if (_configuration.GetSection("ENABLE_REDIS").Value == "false")
            {
                return default;
            }

            var muxer = GetConnectionMultiplexer();
            if (!muxer.IsConnected)
            {
                return default;
            }

            var db = GetRedisDatabase();
            foreach (var ep in muxer.GetEndPoints())
            {
                var server = muxer.GetServer(ep);
                var keys = server.Keys(pattern: pattern).ToArray();

                foreach (var key in keys)
                {
                    try
                    {
                        var cacheValue = await db.StringGetAsync(key);
                        if (cacheValue.HasValue)
                        {
                            var ttl = await db.KeyTimeToLiveAsync(key);
                            var value = JsonConvert.DeserializeObject<T>(cacheValue);
                            list.Add(new
                            {
                                Key = (string)key,
                                Value = value,
                                Ttl = ttl?.TotalSeconds ?? 0
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Erro no GetAllAsync key do cache. - {key} - {ex.Message}");
                    }
                }
            }

            return list;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro no GetAllAsync do cache. - {pattern} - {ex}", ex);
            if (Debugger.IsAttached)
                throw;
        }

        return list;
    }

    public List<object> GetAll<T>(string pattern)
    {
        var list = new List<object>();
        try
        {
            if (_configuration.GetSection("ENABLE_REDIS").Value == "false")
            {
                return default;
            }

            var muxer = GetConnectionMultiplexer();
            if (!muxer.IsConnected)
            {
                return default;
            }

            IDatabase db = GetRedisDatabase();
            foreach (var ep in muxer.GetEndPoints())
            {
                var server = muxer.GetServer(ep);
                var keys = server.Keys(pattern: pattern).ToArray();

                foreach (var key in keys)
                {
                    try
                    {
                        var cacheValue = db.StringGet(key);
                        if (cacheValue.HasValue)
                        {
                            var ttl = db.KeyTimeToLive(key);
                            var value = JsonConvert.DeserializeObject<T>(cacheValue);
                            list.Add(new
                            {
                                Key = (string)key,
                                Value = value,
                                Ttl = ttl?.TotalSeconds ?? 0
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Erro no GetAll key do cache. - {key} - {ex.Message}");
                    }
                }
            }

            return list;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro no GetAll do cache. - {pattern} - {ex}", ex);
            if (Debugger.IsAttached)
                throw;
        }

        return list;
    }

    public async Task<T> GetAsync<T>(string key)
    {
        try
        {
            if (_configuration.GetSection("ENABLE_REDIS").Value == "false")
            {
                return default;
            }

            var muxer = GetConnectionMultiplexer();
            if (!muxer.IsConnected)
            {
                return default;
            }

            var cacheValue = await GetRedisDatabase().StringGetAsync(key);

            if (cacheValue.HasValue)
            {
                var value = JsonConvert.DeserializeObject<T>(cacheValue,
                    new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });
                return value;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro no GetAsync do cache. - {key} - {ex.Message}");
            if (Debugger.IsAttached)
                throw;
        }

        return default;
    }

    public T Get<T>(string key)
    {
        try
        {
            if (_configuration.GetSection("ENABLE_REDIS").Value == "false")
            {
                return default;
            }

            var muxer = GetConnectionMultiplexer();
            if (!muxer.IsConnected)
            {
                return default;
            }

            var cacheValue = GetRedisDatabase().StringGet(key);

            if (cacheValue.HasValue)
            {
                var value = JsonConvert.DeserializeObject<T>(cacheValue);
                return value;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro no Get do cache. - {key} - {ex.Message}");
            if (Debugger.IsAttached)
                throw;
        }

        return default;
    }

    #endregion

    #region UtilizaCache

    public bool UtilizaCache()
    {
        return _configuration.GetSection("ENABLE_REDIS").Value == "true";
    }

    #endregion

    #region Delete

    public async Task DeleteAsync(string key)
    {
        try
        {
            if (_configuration.GetSection("ENABLE_REDIS").Value == "false")
            {
                return;
            }

            var muxer = GetConnectionMultiplexer();
            if (!muxer.IsConnected)
            {
                return;
            }

            IDatabase db = GetRedisDatabase();

            if (key.Contains("*"))
            {
                _logger.LogError("Evite ao máximo utilizar * na key, pois a pesquisa fica EXTREMAMENTE LENTA.");
                foreach (var ep in muxer.GetEndPoints())
                {
                    var server = muxer.GetServer(ep);
                    var keys = server.Keys(pattern: key).ToArray();
                    await db.KeyDeleteAsync(keys, flags: CommandFlags.FireAndForget);
                }
            }

            await db.KeyDeleteAsync(key, flags: CommandFlags.FireAndForget);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro no DeleteAsync do cache. - {key} - {ex.Message}");
            if (Debugger.IsAttached)
                throw;
        }
    }

    public void Delete(string key)
    {
        try
        {
            if (_configuration.GetSection("ENABLE_REDIS").Value == "false")
            {
                return;
            }

            var muxer = GetConnectionMultiplexer();
            if (!muxer.IsConnected)
            {
                return;
            }

            IDatabase db = GetRedisDatabase();
            if (key.Contains("*"))
            {
                _logger.LogError("Evite ao máximo utilizar * na key, pois a pesquisa fica EXTREMAMENTE LENTA.");
                foreach (var ep in muxer.GetEndPoints())
                {
                    var server = muxer.GetServer(ep);
                    var keys = server.Keys(pattern: key).ToArray();
                    db.KeyDelete(keys, flags: CommandFlags.FireAndForget);
                }
            }

            db.KeyDelete(key, flags: CommandFlags.FireAndForget);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro no Delete do cache. - {key} - {ex.Message}");
            if (Debugger.IsAttached)
                throw;
        }
    }

    #endregion

    #region Pub

    public void Pub<TRequestMessage>(string chanel, TRequestMessage message)
    {
        try
        {
            if (_configuration.GetSection("ENABLE_REDIS").Value == "false")
            {
                return;
            }

            var muxer = GetConnectionMultiplexer();
            if (!muxer.IsConnected)
            {
                return;
            }

            var db = GetRedisDatabase();
            db.Publish(chanel, JsonConvert.SerializeObject(message));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro no Pub do cache. - {chanel} - {ex.Message}");
            if (Debugger.IsAttached)
                throw;
        }
    }

    public void Sub<TResponseMessage>(string chanel, Action<TResponseMessage> callback)
    {
        try
        {
            if (_configuration.GetSection("ENABLE_REDIS").Value == "false")
            {
                return;
            }

            var muxer = GetConnectionMultiplexer();
            if (!muxer.IsConnected)
            {
                return;
            }

            var sub = muxer.GetSubscriber();
            sub.Subscribe(chanel, (_, message) =>
            {
                var messageJson = (string)message;
                callback(JsonConvert.DeserializeObject<TResponseMessage>(messageJson));
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro no Pub do cache. - {chanel} - {ex.Message}");
            if (Debugger.IsAttached)
                throw;
        }
    }

    #endregion

    #region Lock

    protected void LockWait(string key, int timeoutInMilliseconds)
    {
        var locked = Get<string>(key);
        if (string.IsNullOrWhiteSpace(locked))
        {
            _logger.LogDebug($"{key} Sem Lock.");
            return;
        }


        var sw = new Stopwatch();
        sw.Start();
        while (!string.IsNullOrWhiteSpace(locked))
        {
            _logger.LogDebug($"{key} lockado, aguardando {timeoutInMilliseconds}ms - {sw.ElapsedMilliseconds}");
            Thread.Sleep(timeoutInMilliseconds);
            locked = Get<string>(key);
        }

        sw.Stop();
        _logger.LogDebug($"{key} liberado - {sw.ElapsedMilliseconds}");
    }

    public void LockWaitAndSet(string key, string value, int timeoutInMilliseconds = 500)
    {
        LockWaitAndSetImpl(key, value, timeoutInMilliseconds, times: 0);
    }

    private void LockWaitAndSetImpl(string key, string value, int timeoutInMilliseconds, int times)
    {
        var maxTimes = 10;
        var expirationInSeconds = 50;
        try
        {
            if (_configuration.GetSection("ENABLE_REDIS").Value == "false")
            {
                return;
            }

            times++;
            LockWait(key, timeoutInMilliseconds);

            var expiry = TimeSpan.FromSeconds(expirationInSeconds);
            if (!GetRedisDatabase().StringSet(key, JsonConvert.SerializeObject(value), expiry, When.NotExists))
            {
                if (times < maxTimes)
                {
                    _logger.LogDebug($"Não conseguiu setar {key}. Tentativa - {times}");
                    LockWaitAndSetImpl(key, value, expirationInSeconds, times);
                }
            }
            else
            {
                _logger.LogDebug($"Setou {key}. Tentativa - {times}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao efetuar lock " + key);
            if (times < maxTimes)
            {
                _logger.LogDebug($"Não conseguiu setar {key}. Tentativa - {times}");
                LockWaitAndSetImpl(key, value, expirationInSeconds, times);
            }
        }
    }

    public void LockDelete(string key, string value = null)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            _logger.LogDebug($"{key} sem verificação de valor, deslockando");
            Delete(key);
        }

        //Se meu processo demorar mais que {expirationInSeconds} a chave vai expirar e outro processo pode lockar.
        //No caso de outro processo lockar, este processo não pode deslockar.
        var atualLocked = Get<string>(key);
        if (atualLocked == value)
        {
            _logger.LogDebug($"{key} com verificação de valor, deslockando");
            Delete(key);
        }
    }

    #endregion

    #region GetOrCreate

    public T? GetOrCreate<T>(string cacheKey, Func<T> factory, int? expirationInSeconds = NextCacheHelper.DefaultExpirationInSeconds)
    {
        var cacheItem = Get<T>(cacheKey);
        if (cacheItem is not null)
        {
            return cacheItem;
        }

        cacheItem = factory();
        Set(cacheKey, cacheItem, expirationInSeconds);
        return cacheItem;
    }

    public async Task<T?> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> factory, int? expirationInSeconds = NextCacheHelper.DefaultExpirationInSeconds)
    {
        var cacheItem = await GetAsync<T>(cacheKey).ConfigureAwait(false);
        if (cacheItem is not null)
        {
            return cacheItem;
        }

        cacheItem = await factory().ConfigureAwait(false);
        await SetAsync(cacheKey, cacheItem, expirationInSeconds).ConfigureAwait(false);
        return cacheItem;
    }

    #endregion
}