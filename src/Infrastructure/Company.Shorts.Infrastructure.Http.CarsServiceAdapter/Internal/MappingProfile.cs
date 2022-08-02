namespace Company.Shorts.Infrastucture.Http.UseHttpClientCorrectly.Internal
{
    using Company.Shorts.Blocks.Common.Mapping.Core;
    using Company.Shorts.Domain;
    using Company.Shorts.Infrastucture.Http.CarsServiceAdapter.Internal.Models;

    internal class MappingProfile : MappingProfileBase
    {
        public MappingProfile()
        {
            CreateMap<CarDto, Car>();
            CreateMap<Car, CreateCarDto>()
                .ForMember(m => m.WeightInKilograms, opt => opt.MapFrom(from => from.Weight))
                .ForMember(m => m.AccelerationInKilometersPerHour, opt => opt.MapFrom(from => from.Acceleration));
        }
    }
}
