namespace Auditoria.Infra.Caching;

public interface INextSingletonCache
{
    /// <summary>
    /// SetAsync é sem escopo, ou seja, sem filtro de Tenant. Atenção ao utilizar.
    /// </summary>
    /// <param name="key">Key para identificar o cache.</param>
    /// <param name="value">Valor do cache</param>
    /// <param name="expirationInSeconds">Tempo em segundos da expiração do cache. Null significa que nunca expira.</param>
    Task SetAsync<T>(string key, T value, int? expirationInSeconds = 300);

    void Set<T>(string key, T value, int? expirationInSeconds = 300);

    /// <summary>
    /// GetAsync é sem escopo, ou seja, sem filtro de Tenant. Atenção ao utilizar.
    /// </summary>
    /// <param name="key">Key para identificar o cache.</param>
    Task<T> GetAsync<T>(string key);

    T Get<T>(string key);

    /// <summary>
    /// GetAllAsync é sem escopo, ou seja, sem filtro de Tenant. Atenção ao utilizar.
    /// </summary>
    /// <param name="pattern">Filtro para identificar o cache. O caracter * funciona como coringa, semelhante ao % do SQL</param>
    Task<List<object>> GetAllAsync<T>(string pattern);

    /// <summary>
    /// GetAll é sem escopo, ou seja, sem filtro de Tenant. Atenção ao utilizar.
    /// </summary>
    /// <param name="pattern">Filtro para identificar o cache. O caracter * funciona como coringa, semelhante ao % do SQL</param>
    List<object> GetAll<T>(string pattern);

    /// <summary>
    /// DeleteAsync é sem escopo, ou seja, sem filtro de Tenant. Atenção ao utilizar.
    /// </summary>
    /// <param name="key">Key para identificar o cache.</param>
    Task DeleteAsync(string key);

    /// <summary>
    /// Delete é sem escopo, ou seja, sem filtro de Tenant. Atenção ao utilizar.
    /// </summary>
    /// <param name="key">Key para identificar o cache.</param>
    void Delete(string key);

    /// <summary>
    /// Publica uma mensagem para posteriormente ser processada pelo "Sub"
    /// </summary>
    /// <param name="chanel">É um canal para publicação (Pub) e escuta (Sub). Trabalhamos com concorrencia de usuários então não devemos utilizar chaves fixas. Uma boa estratégia para chave é um Guid.NewGuid().ToString("D")</param>
    /// <param name="message">É o conteúdo da mensagem, pode ser um texto ou objeto complexo. O "Sub" inscrito no canal receberá esta mensagem.</param>
    void Pub<TRequestMessage>(string chanel, TRequestMessage message);

    /// <summary>
    /// Se inscreve como interessado em uma mensagem, é chamado sempre que uma nova mensagem é recebida.
    /// </summary>
    /// <param name="chanel">É um canal para publicação (Pub) e escuta (Sub). Deve ser o mesmo canal informado na hora da publicação (Pub) </param>
    /// <param name="callback">Sempre que uma nova mensagem for recebida, a função de callback será chamada, o parametro TResponseMessage é o conteúdo do "Pub"</param>
    void Sub<TResponseMessage>(string chanel, Action<TResponseMessage> callback);

    void LockWaitAndSet(string key, string value, int timeoutInMilliseconds = 500);
    void LockDelete(string key, string value = null);
    bool UtilizaCache();

    /// <summary>
    /// Retorna um objeto relacionado a chave indicada. Caso não encontre no cache, criará um registro com o retorno da função factory
    /// </summary>
    /// <param name="cacheKey">Valor da chave do cache</param>
    /// <param name="factory">Função que retorna um objeto de T</param>
    /// <param name="expirationInSeconds">Tempo de vida da chave no cache, em segundos.</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T? GetOrCreate<T>(string cacheKey, Func<T> factory, int? expirationInSeconds = NextCacheHelper.DefaultExpirationInSeconds);

    /// <summary>
    /// Retorna um objeto relacionado a chave indicada. Caso não encontre no cache, criará um registro com o retorno da função factory
    /// </summary>
    /// <param name="cacheKey">Valor da chave do cache</param>
    /// <param name="factory">Função que retorna um objeto de T</param>
    /// <param name="expirationInSeconds">Tempo de vida da chave no cache, em segundos.</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task<T?> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> factory, int? expirationInSeconds = NextCacheHelper.DefaultExpirationInSeconds);
}