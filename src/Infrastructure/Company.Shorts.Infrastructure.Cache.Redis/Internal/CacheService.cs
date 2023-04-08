namespace Company.Shorts.Infrastructure.Cache.Redis.Internal
{
    using Company.Shorts.Application.Contracts.Cache;
    using Microsoft.Extensions.Caching.Distributed;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    internal sealed class CacheService : ICacheService
    {
        private readonly IDistributedCache cache;

        public CacheService(IDistributedCache cache)
        {
            this.cache = cache;
        }

        public async Task<T> GetOrAdd<T>(string key, Func<Task<T>> fun)
        {
            var item = await this.cache.GetAsync(key);

            if (item is not null)
            {
                return JsonSerializer.Deserialize<T>(item);
            }
       
            var items = await fun();

            var data = JsonSerializer.Serialize(items);
            var byteData = Encoding.UTF8.GetBytes(data);

            await this.cache.SetAsync(key, byteData);

            return items;
        }
    }
}
