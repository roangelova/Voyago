using System.Text.Json;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace GetMyTicket.Service.Caching
{
    public class RedisCachingService : ICachingService
    {
        private readonly ConnectionMultiplexer redis;
        private readonly IDatabase database;

        public RedisCachingService(IConfiguration config)
        {
            var connectionString = config.GetRequiredSection("Redis");
            redis = ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                EndPoints = { { connectionString["EndPoints:0:Host"], int.Parse(connectionString["EndPoints:0:Port"]) } },
                User = connectionString["User"],
                Password = connectionString["Password"]
            });
            database = this.redis.GetDatabase();
        }
        public async Task<T> Get<T>(string key)
        {
            var value = await this.database.StringGetAsync(key);

            if (!value.HasValue)
                return default;

            return JsonSerializer.Deserialize<T>(value);
        }

        public async Task Remove(string key)
        {
            await this.database.KeyDeleteAsync(key);
        }

        public async Task<T> Set<T>(string key, T value, TimeSpan? expiresAt = null)
        {
            await this.database.StringSetAsync(key, JsonSerializer.Serialize(value), expiresAt.Value);
            return value;
        }

        public async Task<T> Update<T>(string key, T value, TimeSpan? expiresAt = null)
        {
            await Remove(key);
            return await Set(key, value, expiresAt);
        }
    }
}
