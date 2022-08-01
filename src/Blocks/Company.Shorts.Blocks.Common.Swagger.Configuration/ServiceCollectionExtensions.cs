namespace Company.Shorts.Blocks.Common.Swagger.Configuration
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.AspNetCore.Mvc.Versioning;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Swashbuckle.AspNetCore.Filters;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System;
    using System.Reflection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwaggerConfiguration(
            this IServiceCollection services,
            Action<ApiVersioningOptions>? versioningOptions = null,
            Action<ApiExplorerOptions>? explorerOptions = null,
            Action<SwaggerGenOptions>? swaggerOptions = null,
            bool useNewtonsoftSerialization = true,
            params Assembly[]? swaggerExampleAssemblies)
        {
            versioningOptions ??= options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            };

            explorerOptions ??= options =>
            {
                // Formats version as "'v'[major][.minor][-status]".
                options.GroupNameFormat = "'v'VV";
                options.SubstituteApiVersionInUrl = true;
            };

            swaggerOptions ??= options =>
            {
                options.EnableAnnotations();
                options.ExampleFilters();
                options.OperationFilter<SwaggerDefaultValues>();
                options.OperationFilter<SwaggerHeaderFilter>();

                foreach (var xmlFile in Directory.GetFiles(AppContext.BaseDirectory, "*.xml"))
                {
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    options.IncludeXmlComments(xmlPath, true);
                }
            };

            services
                .AddApiVersioning(versioningOptions)
                .AddVersionedApiExplorer(explorerOptions)
                .AddSwaggerExamplesFromAssemblies(swaggerExampleAssemblies)
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
                .AddSwaggerGen(swaggerOptions);

            if (useNewtonsoftSerialization)
            {
                services.AddSwaggerGenNewtonsoftSupport();
            }

            return services;
        }
    }
}