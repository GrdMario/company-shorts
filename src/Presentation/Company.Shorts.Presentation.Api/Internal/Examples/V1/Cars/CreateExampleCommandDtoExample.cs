namespace Company.Shorts.Presentation.Api.Internal.Examples.V1.Cars
{
    using Company.Shorts.Presentation.Api.Controllers.V1.Models.Cars;
    using Swashbuckle.AspNetCore.Filters;

    internal class CreateExampleCommandDtoExample : IExamplesProvider<CreateCarsCommandDto>
    {
        public CreateCarsCommandDto GetExamples()
        {
            return new("BMW", 1, 1, 1, 1, DateTimeOffset.UtcNow, "USA");
        }
    }
}
