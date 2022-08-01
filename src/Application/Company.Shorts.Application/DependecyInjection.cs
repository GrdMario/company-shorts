namespace Company.Shorts.Application
{
    using Company.Shorts.Blocks.Application.Contracts;
    using Company.Shorts.Blocks.Application.Core.Adapters;
    using Company.Shorts.Blocks.Application.Core.Behaviors;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using System.Reflection;

    public static class DependecyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddApplicationConfiguration(Assembly.GetExecutingAssembly());

            return services;
        }

        private static IServiceCollection AddApplicationConfiguration(
            this IServiceCollection services,
            params Assembly[] assemblies)
        {
            services.AddMediatR(assemblies);
            services.AddValidatorsFromAssemblies(assemblies, includeInternalTypes: true);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddHttpContextAccessor();

            services.TryAddSingleton<IHttpContextAccessorAdapter, HttpContextAccessorAdapter>();

            return services;
        }
    }
}
