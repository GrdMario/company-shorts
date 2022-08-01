namespace Company.Shorts.Blocks.Common.Mapping.Configuration
{
    using Company.Shorts.Blocks.Common.Extensions;
    using Company.Shorts.Blocks.Common.Mapping.Core;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMapperConfiguration(
            this IServiceCollection services,
            AppDomain appDomain,
            params Assembly[] additionalAssemblies)
        {
            IEnumerable<Assembly> assemblies =
                appDomain
                    .GetAssemblies()
                    .Where(assembly =>
                        assembly
                            .GetTypes()
                            .Any(TypeDependesOnAutoMapper))
                    .ToArray();

            if (additionalAssemblies is not null)
            {
                assemblies = assemblies.Concat(additionalAssemblies);
            }

            services.AddAutoMapper(assemblies);

            return services;
        }

        private static bool TypeDependesOnAutoMapper(Type type)
        {
            if (type.IsAbstract)
            {
                return true;
            }

            if (type.IsAssignableFrom(typeof(MappingProfileBase)))
            {
                return true;
            }

            return type.IsImplementationOf(typeof(IMapFrom<>)) || type.IsImplementationOf(typeof(IMapTo<>));
        }
    }
}