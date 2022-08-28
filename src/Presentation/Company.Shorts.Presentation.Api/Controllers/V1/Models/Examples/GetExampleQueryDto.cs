namespace Company.Shorts.Presentation.Api.Controllers.V1.Models.Examples
{
    public class GetExampleQueryDto : IApiDto
    {
        public GetExampleQueryDto(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; set; }
    }
}
