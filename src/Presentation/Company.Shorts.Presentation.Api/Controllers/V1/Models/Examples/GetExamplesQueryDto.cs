namespace Company.Shorts.Presentation.Api.Controllers.V1.Models.Examples
{
    using Swashbuckle.AspNetCore.Annotations;

    [SwaggerSchema(Description = "Request for fetching examples.")]
    public class GetExamplesQueryDto : IApiDto
    {
        public GetExamplesQueryDto()
        {
        }

        public GetExamplesQueryDto(string? name, int? skip, int? take)
        {
            this.Name = name;
            this.Skip = skip;
            this.Take = take;
        }

        [SwaggerSchema(Description = "Query by name.")]
        [SwaggerParameter]
        public string? Name { get; set; }

        [SwaggerSchema(Description = "Amount of items to skip.")]
        [SwaggerParameter]
        public int? Skip { get; set; } = 0;

        [SwaggerSchema(Description = "Amount of items to take.")]
        [SwaggerParameter]
        public int? Take { get; set; } = 20;
    }
}
