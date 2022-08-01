namespace Company.Shorts.Blocks.Common.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            var hasMatchingGenericTypeInterface = givenType
                .GetInterfaces()
                .Any(interfaceType =>
                    interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == genericType);

            if (hasMatchingGenericTypeInterface)
            {
                return true;
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            Type? baseType = givenType.BaseType;

            return baseType is not null && IsAssignableToGenericType(baseType, genericType);
        }

        public static bool IsImplementationOf(this Type givenType, Type interfaceType)
        {
            return givenType
                .GetInterfaces()
                .Any(interfaceType.Equals);
        }
    }
}