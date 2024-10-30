using System.Text.Json;
using StackExchange.Redis;
using EventsWebApplication.Domain.Interfaces;

namespace EventsWebApplication.Infrastructure.Data
{
    public class CacheRepository(
        IConnectionMultiplexer redis) : ICacheRepository
    {
        private readonly IDatabase redisDatabase = redis.GetDatabase();

        public async Task SetAsync<TObject>(string key, TObject value, TimeSpan expiresIn)
        {
            string serializedData = JsonSerializer.Serialize(value);
            await redisDatabase.StringSetAsync(Key<TObject>(key), serializedData, expiresIn);
        }

        public async Task<TObject?> GetAsync<TObject>(string key)
        {
            var redisValue = await redisDatabase.StringGetAsync(Key<TObject>(key));
            return redisValue.HasValue ? JsonSerializer.Deserialize<TObject>(redisValue.ToString()) : default;
        }

        public async Task DeleteAsync<TObject>(string key)
        {
            await redisDatabase.KeyDeleteAsync(Key<TObject>(key));
        }

        private string Key<TObject>(string key) => $"{typeof(TObject).Name}::{key}";
    }
}