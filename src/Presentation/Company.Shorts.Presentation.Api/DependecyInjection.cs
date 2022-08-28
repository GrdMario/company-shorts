namespace Company.Shorts.Presentation.Api
{
    using Company.Shorts.Presentation.Api.Internal.Configuration.Rest;
    using Company.Shorts.Presentation.Api.Internal.Configuration.Swagger;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class DependecyInjection
    {
        public static IServiceCollection AddPresentationLayer(this IServiceCollection services, IHostEnvironment environment)
        {
            services
                .AddSwaggerConfiguration();

            services
                .AddRestApiConfiguration(environment);

            return services;
        }
    }
}
