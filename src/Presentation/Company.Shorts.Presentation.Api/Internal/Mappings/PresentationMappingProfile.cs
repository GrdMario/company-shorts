namespace Company.Shorts.Presentation.Api.Internal.Mappings
{
    using Company.Shorts.Application.ExampleAggregate.Command;
    using Company.Shorts.Application.ExampleAggregate.Query;
    using Company.Shorts.Application.UserAggregate.Command;
    using Company.Shorts.Application.UserAggregate.Query;
    using Company.Shorts.Blocks.Common.Mapping.Core;
    using Company.Shorts.Presentation.Api.Controllers.V1.Models.Examples;

    internal sealed class PresentationMappingProfile : MappingProfileBase
    {
        public PresentationMappingProfile()
        {
            CreateMap<GetExampleQueryDto, GetExampleQuery>();
            CreateMap<GetExamplesQueryDto, GetExamplesQuery>();
            CreateMap<CreateExampleCommandDto, CreateExampleCommand>();
            CreateMap<UpdateExampleCommandDto, UpdateExampleCommand>();

            CreateMap<GetUsersQueryDto, GetUsersQuery>();
            CreateMap<CreateUserCommandDto, CreateUserCommand>();
        }
    }
}
