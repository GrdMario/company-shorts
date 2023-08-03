namespace Company.Shorts.Application.Internal.Mappings
{
    using Company.Shorts.Application.Files.Models;
    using Company.Shorts.Blocks.Common.Mapping.Core;
    using Company.Shorts.Domain;

    internal sealed class ApplicationMappingProfile : MappingProfileBase
    {
        public ApplicationMappingProfile()
        {
            this.CreateMap<File, FileResponse>();
            this.CreateMap<File, FileDownloadResponse>();
        }
    }
}
