namespace Company.Shorts.Blocks.Common.Mapping.Core
{
    using AutoMapper;
    using System.Reflection;

    public abstract class MappingProfileBase : Profile
    {
        protected MappingProfileBase()
        {
            ApplyMappingsFromAssembly(Assembly.GetCallingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly
                .GetExportedTypes()
                .Where(type =>
                    type.GetInterfaces()
                        .Any(interfaceType =>
                            interfaceType.IsGenericType &&
                            (interfaceType.GetGenericTypeDefinition() == typeof(IMapFrom<>) ||
                            interfaceType.GetGenericTypeDefinition() == typeof(IMapTo<>))))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                MethodInfo? methodInfo = type.GetMethod("Mapping")
                    ?? type.GetInterface("IMapFrom`1`")?.GetMethod("Mapping")
                    ?? type.GetInterface("IMapTo`1`")?.GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}