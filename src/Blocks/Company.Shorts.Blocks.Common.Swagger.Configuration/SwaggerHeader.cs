namespace Company.Shorts.Blocks.Common.Swagger.Configuration
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SwaggerHeader : Attribute
    {
        public SwaggerHeader(
            string name,
            string? description,
            string? defaultValue,
            bool isRequired = false)
        {
            Name = name;
            Description = description ?? string.Empty;
            DefaultValue = defaultValue ?? string.Empty;
            IsRequired = isRequired;
        }

        public string Name { get; }

        public string Description { get; }

        public string DefaultValue { get; }

        public bool IsRequired { get; }
    }
}