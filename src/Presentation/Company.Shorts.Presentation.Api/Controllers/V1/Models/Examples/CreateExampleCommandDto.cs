namespace Company.Shorts.Presentation.Api.Controllers.V1.Models.Examples
{
    using Swashbuckle.AspNetCore.Annotations;

    [SwaggerSchema(Description = "Request for creating an example.")]
    public class CreateExampleCommandDto : IApiDto
    {
        public CreateExampleCommandDto(string name)
        {
            this.Name = name;
        }

        [SwaggerSchema(Description = "Name of the example", Nullable = false, ReadOnly = true)]
        public string Name { get; set; } = default!;
    }
}
