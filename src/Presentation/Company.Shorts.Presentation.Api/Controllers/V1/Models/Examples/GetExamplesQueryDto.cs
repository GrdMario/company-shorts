namespace Company.Shorts.Presentation.Api.Controllers.V1.Models.Examples
{
    public record GetExampleQueryDto(Guid Id): IApiDto;
    public record GetExamplesQueryDto(string? Name, int? Page, int? Size) : IApiDto;
    public record CreateExampleCommandDto(string Name, bool ThrowCustomException, bool ThrowNotFoundException, bool ThorwInternalException) : IApiDto;
    public record UpdateExampleCommandDto(string Name) : IApiDto;
}
