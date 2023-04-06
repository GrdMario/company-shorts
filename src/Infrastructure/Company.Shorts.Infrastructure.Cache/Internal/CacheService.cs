namespace Company.Shorts.Infrastructure.Cache.Internal
{
    using Company.Shorts.Application.Contracts.Cache;
    using Microsoft.Extensions.Caching.Memory;

    internal sealed class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetOrAdd<T>(string key, Func<Task<T>> fun)
        {
            var item = this._cache.Get<T>(key);

            if (item is null)
            {
                item = await fun();

                this._cache.Set(key, item);
            }

            return item;
        }
    }
}
