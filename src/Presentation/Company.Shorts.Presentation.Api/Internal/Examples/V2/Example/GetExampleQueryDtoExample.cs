namespace Company.Shorts.Presentation.Api.Internal.Examples.V2.Example
{
    using Company.Shorts.Presentation.Api.Controllers.V2.Models.Examples;
    using Swashbuckle.AspNetCore.Filters;
    using System;

    internal sealed class GetExampleQueryDtoExample : IExamplesProvider<GetExampleQueryDto>
    {
        public GetExampleQueryDto GetExamples()
        {
            return new(Guid.NewGuid());
        }
    }
}
