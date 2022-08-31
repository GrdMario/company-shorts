namespace Company.Shorts.Blocks.Presentation.Api.Configuration
{
    using Company.Shorts.Blocks.Common.Exceptions;
    using Hellang.Middleware.ProblemDetails;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
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
            IHostEnvironment enviroment)
        {
            services
                .AddRouting()
                .AddProblemDetails(options => SetProblemDetailsOptions(options, enviroment))
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new ValueTrimConverter());
                });

            return services;
        }

        private static void SetProblemDetailsOptions(ProblemDetailsOptions options, IHostEnvironment enviroment)
        {
            Type[] knownExceptionTypes = new[] { typeof(ValidationException), typeof(NotFoundException) };

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