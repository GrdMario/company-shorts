namespace Company.Shorts.Presentation.Api
{
    using Company.Shorts.Blocks.Common.Swagger.Configuration;
    using Company.Shorts.Blocks.Presentation.Api.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Swashbuckle.AspNetCore.Filters;
    using System.Reflection;

    public static class DependecyInjection
    {
        public static IServiceCollection AddPresentationLayer(this IServiceCollection services, IHostEnvironment environment)
        {
            services
                .AddSwaggerConfiguration();

            services
                .AddRestApiConfiguration(environment)
                .AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
