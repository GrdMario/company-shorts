namespace Company.Shorts.Presentation.Api.Internal.Configuration.Rest
{
    using Company.Shorts.Blocks.Common.Exceptions;
    using Hellang.Middleware.ProblemDetails;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using Microsoft.AspNetCore.Mvc.Versioning;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Linq;
    using System.Reflection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRestApiConfiguration(
            this IServiceCollection services,
            IHostEnvironment enviroment)
        {
            Type[] knownExceptionTypes = new Type[] { typeof(ValidationException), typeof(NotFoundException) };

            services
                .AddRouting(options =>
                {
                    options.LowercaseUrls = true;
                })
                .AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                    options.ApiVersionReader = new UrlSegmentApiVersionReader();
                })
                .AddVersionedApiExplorer(options =>
                {
                    // Formats version as "'v'[major][.minor][-status]".
                    options.GroupNameFormat = "'v'VV";
                    options.SubstituteApiVersionInUrl = true;
                })
                .AddProblemDetails(options =>
                {
                    SetProblemDetailsOptions(options, enviroment, knownExceptionTypes);
                })
                .AddControllers(options =>
                {
                    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
                });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }

        private static void SetProblemDetailsOptions(ProblemDetailsOptions options, IHostEnvironment enviroment, Type[] knownExceptionTypes)
        {
            options.IncludeExceptionDetails = (_, exception) =>
                enviroment.IsDevelopment() &&
                !knownExceptionTypes.Contains(exception.GetType());

            options.Map<ValidationException>(exception =>
                new ValidationProblemDetails(exception.Errors)
                {
                    Title = exception.Title,
                    Detail = exception.Detail,
                    Status = StatusCodes.Status400BadRequest
                });

            options.Map<NotFoundException>(exception =>
                new ValidationProblemDetails
                {
                    Title = exception.Title,
                    Detail = exception.Detail,
                    Status = StatusCodes.Status404NotFound
                });
        }
    }
}