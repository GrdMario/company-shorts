﻿namespace Company.Shorts.Infrastructure.Cache.Internal
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

        public T Add<T>(string key, T item)
        {
            return this._cache.Set(key, item);
        }

        public T Get<T>(string key)
        {
            return this._cache.Get<T>(key);
        }
    }
}
