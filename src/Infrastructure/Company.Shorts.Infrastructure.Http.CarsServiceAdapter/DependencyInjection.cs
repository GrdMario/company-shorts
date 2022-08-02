namespace Company.Shorts.Infrastructure.Http.CarsServiceAdapter
{
    using Company.Shorts.Application.Contracts.Http;
    using Company.Shorts.Infrastructure.Http.CarsServiceAdapter.Internal;
    using Company.Shorts.Infrastucture.Http.CarsServiceAdapter.Internal;
    using Company.Shorts.Infrastucture.Http.CarsServiceAdapter.Internal.Clients;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureHttpCarServiceAdapter(this IServiceCollection services, CarServiceAdapterSettings settings)
        {
            services
                .AddTransient<DefaultHttpDelegatingHandler>();

            services
                .AddHttpClient<ICompanyCarsHttpClient, CompanyCarsHttpClient>(opt => opt.BaseAddress = new Uri(settings.Url))
                .AddHttpMessageHandler<DefaultHttpDelegatingHandler>();
            
            services
                .AddScoped<ICompanyCarsAdapter, CompanyCarsAdapter>();
            
            return services;
        }
    }

    public class CarServiceAdapterSettings
    {
        public const string Key = nameof(CarServiceAdapterSettings);

        public string Url { get; set; } = default!;
    }
}