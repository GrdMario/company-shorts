namespace Company.Shorts.Presentation.Api.Internal.Mappings
{
    using AutoMapper;
    using Company.Shorts.Application.ExampleAggregate.Command;
    using Company.Shorts.Application.ExampleAggregate.Common.Responses;
    using Company.Shorts.Application.ExampleAggregate.Query;
    using Company.Shorts.Presentation.Api.Controllers.V2.Models.Examples;

    internal sealed class PresentationMappingProfileV2 : Profile
    {
        public PresentationMappingProfileV2()
        {
            this.CreateMap<GetExampleQueryDto, GetExampleQuery>();
            this.CreateMap<GetExamplesQueryDto, GetExamplesQuery>();
            this.CreateMap<CreateExampleCommandDto, CreateExampleCommand>();
            this.CreateMap<UpdateExampleCommandDto, UpdateExampleCommand>();
            this.CreateMap<ExampleResponse, ExampleResponseDto>();
        }
    }
}
