namespace Company.Shorts.Presentation.Api.Controllers.V1.Models.Cars
{
    public record GetCarsQueryDto() : IApiDto;

    public record CreateCarsCommandDto(
        string Name,
        int Consumption,
        int HorsePower,
        int Weight,
        int Acceleration,
        DateTimeOffset Year,
        string Origin) : IApiDto;
}
