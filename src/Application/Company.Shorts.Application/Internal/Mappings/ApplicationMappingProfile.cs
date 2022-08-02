namespace Company.Shorts.Application.Internal.Mappings
{
    using Company.Shorts.Application.CarAggregate.Common;
    using Company.Shorts.Application.ExampleAggregate.Common.Responses;
    using Company.Shorts.Blocks.Common.Mapping.Core;
    using Company.Shorts.Domain;

    internal sealed class ApplicationMappingProfile : MappingProfileBase
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Example, ExampleResponse>();
            CreateMap<Car, CarResponse>();
        }
    }
}
