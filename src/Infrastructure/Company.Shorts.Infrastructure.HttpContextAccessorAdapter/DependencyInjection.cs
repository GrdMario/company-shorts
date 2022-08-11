namespace Company.Shorts.Infrastructure.HttpContextAccessorAdapter
{
    using Company.Shorts.Application.Contracts.HttpContextAccessor;
    using Company.Shorts.Infrastructure.HttpContextAccessorAdapter.Internal;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddHttpContextAdapter(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<IHttpContextAccessorAdapter, DefaultHttpContextAccessorAdapter>();

            return services;
        }
    }
}