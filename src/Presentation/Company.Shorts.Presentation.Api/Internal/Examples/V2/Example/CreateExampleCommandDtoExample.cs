namespace Company.Shorts.Presentation.Api.Internal.Examples.V2.Example
{
    using Company.Shorts.Presentation.Api.Controllers.V2.Models.Examples;
    using Swashbuckle.AspNetCore.Filters;

    internal sealed class CreateExampleCommandDtoExample : IExamplesProvider<CreateExampleCommandDto>
    {
        public CreateExampleCommandDto GetExamples()
        {
            return new("My name.");
        }
    }
}
