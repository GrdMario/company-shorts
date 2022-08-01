﻿namespace Company.Shorts.Presentation.Api.Internal.Examples.V1.Example
{
    using Company.Shorts.Presentation.Api.Controllers.V1.Models.Examples;
    using Swashbuckle.AspNetCore.Filters;

    internal sealed class UpdateExampleCommandDtoExample : IExamplesProvider<UpdateExampleCommandDto>
    {
        public UpdateExampleCommandDto GetExamples()
        {
            return new("My Name");
        }
    }
}
