namespace Company.Shorts.Presentation.Api.Internal.Examples.V1.Cars
{
    using Swashbuckle.AspNetCore.Filters;

    internal class GetCarsQueryDtoExample : IExamplesProvider<GetCarsQueryDtoExample>
    {
        public GetCarsQueryDtoExample GetExamples()
        {
            return new();
        }
    }
}
