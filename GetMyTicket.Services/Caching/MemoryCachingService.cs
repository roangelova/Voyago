using Microsoft.Extensions.Caching.Memory;

namespace GetMyTicket.Service.Caching
{
    public class MemoryCachingService : ICachingService
    {
        private readonly IMemoryCache memoryCache;

        public MemoryCachingService(IMemoryCache memoryCache) 
        {
            this.memoryCache = memoryCache;
        }

        public Task<T> Get<T>(string key)
        {
            memoryCache.TryGetValue(key, out T result);

            return Task.FromResult(result);
        }

        public Task Remove(string key)
        {
            if (memoryCache.TryGetValue(key, out var result))
            {
                memoryCache.Remove(key);
            }

            return Task.CompletedTask;
        }

        public Task<T> Set<T>(string key, T value, TimeSpan? expiresAt = null)
        {
            if (expiresAt == null)
                return Task.FromResult(memoryCache.Set(key, value));
            else
                return Task.FromResult(memoryCache.Set(key, value, expiresAt.Value));
        }

        public async Task<T> Update<T>(string key, T value, TimeSpan? expiresAt = null)
        {
            await this.Remove(key);
            return await this.Set(key, value, expiresAt);
        }
    }
}
