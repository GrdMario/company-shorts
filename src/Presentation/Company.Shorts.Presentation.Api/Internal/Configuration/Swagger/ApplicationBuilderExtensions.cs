namespace Company.Shorts.Presentation.Api.Internal.Configuration.Swagger
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Exstension class for <see cref="IApplicationBuilder"/>.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder builder)
        {
            IApiVersionDescriptionProvider provider = builder.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            builder
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.DisplayOperationId();
                    options.DisplayRequestDuration();

                    foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                });

            return builder;
        }
    }
}