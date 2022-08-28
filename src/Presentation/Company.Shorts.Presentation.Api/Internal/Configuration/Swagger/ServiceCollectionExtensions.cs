namespace Company.Shorts.Presentation.Api.Internal.Configuration.Swagger
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Swashbuckle.AspNetCore.Filters;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System.Reflection;

    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services
                .AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly())
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
                .AddSwaggerGen(options =>
                {
                    options.EnableAnnotations();
                    options.ExampleFilters();
                    options.OperationFilter<SwaggerDefaultValues>();

                    foreach (var xmlFile in Directory.GetFiles(AppContext.BaseDirectory, "*.xml"))
                    {
                        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                        options.IncludeXmlComments(xmlPath, true);
                    }
                });

            return services;
        }
    }
}
