namespace LibraryBackend.Application.Interfaces.Infrastructure.Cache;

public interface ICacheService
{
    Task<T?> GetCacheData<T>(string key);

    Task SetCacheData<T>(string key, T value);
    Task RemoveCache(string key);
}