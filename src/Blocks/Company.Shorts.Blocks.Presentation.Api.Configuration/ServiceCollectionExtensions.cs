namespace Company.Shorts.Blocks.Presentation.Api.Configuration
{
    using Company.Shorts.Blocks.Common.Exceptions;
    using Hellang.Middleware.ProblemDetails;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Linq;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRestApiConfiguration(
            this IServiceCollection services,
            IHostEnvironment enviroment,
            Action<RouteOptions>? routeOptions = null,
            Action<Hellang.Middleware.ProblemDetails.ProblemDetailsOptions>? problemDetailsOptions = null,
            Action<MvcOptions>? mvcOptions = null,
            Action<MvcNewtonsoftJsonOptions>? newtonsoftOptions = null,
            Type[]? knownExceptionTypes = null)
        {
            routeOptions ??= options => options.LowercaseUrls = true;

            knownExceptionTypes ??= new[] { typeof(ValidationException) };

            problemDetailsOptions ??= options => SetProblemDetailsOptions(options, enviroment, knownExceptionTypes);

            mvcOptions ??= options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            };

            newtonsoftOptions ??= options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Converters.Add(new ValueTrimConverter());
            };

            services
                .AddRouting(routeOptions)
                .AddProblemDetails(problemDetailsOptions)
                .AddControllers(mvcOptions)
                .AddNewtonsoftJson(newtonsoftOptions);

            return services;
        }

        private static void SetProblemDetailsOptions(Hellang.Middleware.ProblemDetails.ProblemDetailsOptions options, IHostEnvironment enviroment, Type[] knownExceptionTypes)
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