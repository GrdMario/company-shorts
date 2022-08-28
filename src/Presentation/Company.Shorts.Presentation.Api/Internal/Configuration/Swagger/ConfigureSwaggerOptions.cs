namespace Company.Shorts.Presentation.Api.Internal.Configuration.Swagger
{
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;
        private readonly IConfiguration configuration;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IConfiguration configuration)
        {
            this.provider = provider;
            this.configuration = configuration;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    name: description.GroupName,
                    info: CreateInfoForApiVersion(description));
            }
        }

        private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            SwaggerInfo? swaggerInfo = this.configuration
                .GetSection(nameof(SwaggerInfo))
                .Get<SwaggerInfo>();

            var info = new OpenApiInfo()
            {
                Title = swaggerInfo.Title ?? "Api Documentation",
                Version = description.ApiVersion.ToString(),
                Description = swaggerInfo.Description ?? string.Empty
            };

            if (swaggerInfo.Contact is not null)
            {
                info.Contact = new OpenApiContact()
                {
                    Name = swaggerInfo.Contact.Name,
                    Email = swaggerInfo.Contact.Email,
                    Url = new Uri(swaggerInfo.Contact.Url)
                };
            }

            if (swaggerInfo.Licence is not null)
            {
                info.License = new OpenApiLicense()
                {
                    Name = swaggerInfo.Licence.Name,
                    Url = new Uri(swaggerInfo.Licence.Url)
                };
            }

            return info;
        }
    }
}