namespace Company.Shorts.Presentation.Api.Controllers.V1.Models.Examples
{
    using Swashbuckle.AspNetCore.Annotations;
    using System;

    [SwaggerSchema(Description = "Example response.")]
    public class ExampleResponseDto
    {
       [SwaggerSchema(Description = "Unique identifier of an example.")]
        public Guid Id { get; set; }

        [SwaggerSchema(Description = "Name of an example.")]
        public string Name { get; set; } = default!;
    }
}
