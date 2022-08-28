namespace Company.Shorts.Presentation.Api.Controllers.V2.Models.Examples
{
    public class GetExamplesQueryDto : IApiDto
    {
        public GetExamplesQueryDto(string? name, int? page, int? size)
        {
            Name = name;
            Page = page;
            Size = size;
        }

        public string? Name { get; set; }
        public int? Page { get; set; }

        public int? Size { get; set; }
    }
}
