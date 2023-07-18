namespace Company.Shorts.Infrastructure.Http.ExternalApi
{
    using Company.Shorts.Application.Contracts.Http;
    using Company.Shorts.Infrastructure.Http.ExternalApi.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public static class DependencyInjection
    {
        public static IServiceCollection AddHttpExternalApiModule(this IServiceCollection services, ExternalApiSettings settings)
        {
            services.AddHttpClient<IExternalApi, ExternalApiService>(opt => opt.BaseAddress = new Uri(settings.Url));

            //services.AddScoped<IExternalApi, ExternalApiService>();

            return services;
        }
    }

    public class ExternalApiSettings
    {
        public const string Key = nameof(ExternalApiSettings);

        public string Url { get; set; } = default!;
    }
}