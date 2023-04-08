namespace Company.Shorts.Infrastructure.Cache.Redis
{
    using Company.Shorts.Application.Contracts.Cache;
    using Company.Shorts.Infrastructure.Cache.Redis.Internal;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependecyInjection
    {
        public static IServiceCollection AddReddisCache(this IServiceCollection services, RedisAdapterSettings settings)
        {
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = settings.ConnectionString;
            });

            services.AddScoped<ICacheService, CacheService>();

            return services;
        }
    }

    public class RedisAdapterSettings
    {
        public const string Key = nameof(RedisAdapterSettings);

        public string ConnectionString { get; set; } = default!;

    }
}