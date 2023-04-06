namespace Company.Shorts.Infrastructure.Cache
{
    using Company.Shorts.Application.Contracts.Cache;
    using Company.Shorts.Infrastructure.Cache.Internal;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddCache(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped<ICacheService, CacheService>();
            return services;
        }
    }
}