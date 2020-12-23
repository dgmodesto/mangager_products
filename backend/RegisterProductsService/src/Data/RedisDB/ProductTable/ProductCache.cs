using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.RedisDB.ProductTable
{
    public static class ProductCache
    {
        public static async Task<T> GetCacheValueAsync<T>(this IDistributedCache cache, string key) where T : class
        {
            string result = await cache.GetStringAsync(key);
            if (String.IsNullOrEmpty(result))
            {
                return null;
            }
            var deserializedObj = JsonConvert.DeserializeObject<T>(result);
            return deserializedObj;
        }

        public static async Task SetCacheValueAsync<T>(this IDistributedCache cache, string key, T value) where T : class
        {
            DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions();

            // Remove item from cache after duration
            cacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);

            // Remove item from cache if unsued for the duration
            cacheEntryOptions.SlidingExpiration = TimeSpan.FromSeconds(30);

            string result =  JsonConvert.SerializeObject(value);

            await cache.SetStringAsync(key, result);
        }
    }
}
