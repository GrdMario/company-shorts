namespace Company.Shorts.Blocks.Common.Extensions
{
    using System.Reflection;

    public static class PropertyInfoExtensions
    {
        public static bool HasAttribute<T>(this PropertyInfo property)
            where T : Attribute
        {
            T? attribute = property.GetCustomAttribute<T>(inherit: true);

            return attribute is not null;
        }
    }
}