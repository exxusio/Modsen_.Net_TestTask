using System.Text.Json;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using EventsWebApplication.Application.Abstractions.Caching;
using EventsWebApplication.Domain.Exceptions;

namespace EventsWebApplication.Infrastructure.Caching
{
    public class RedisCacheService(
        IConfiguration configuration,
        IConnectionMultiplexer redis
    ) : ICacheService
    {
        private readonly IDatabase redisDatabase = redis.GetDatabase();

        public async Task SetAsync<TObject>(string key, TObject value)
        {
            TimeSpan expiresIn = TimeSpan.FromMinutes(GetCacheSetting<int>(typeof(TObject).Name));
            string serializedData = JsonSerializer.Serialize(value);

            await redisDatabase.StringSetAsync(Key<TObject>(key), serializedData, expiresIn);
        }

        public async Task<TObject?> GetAsync<TObject>(string key)
        {
            var redisValue = await redisDatabase.StringGetAsync(Key<TObject>(key));
            return redisValue.HasValue
                ? JsonSerializer.Deserialize<TObject>(redisValue.ToString())
                : default;
        }

        public async Task DeleteAsync<TObject>(string key)
        {
            await redisDatabase.KeyDeleteAsync(Key<TObject>(key));
        }

        private string Key<TObject>(string key) => $"{typeof(TObject).Name}::{key}";

        private T GetCacheSetting<T>(string key)
        {
            var value = configuration.GetValue<T>($"CacheSettings:{key}ExpiresInMinutes");
            if (value == null)
            {
                throw new NotFoundException(
                    "The configuration key is missing or null",
                    typeof(T).Name,
                    nameof(key),
                    key
                );
            }
            return value;
        }
    }
}