namespace Company.Shorts.Blocks.Common.Mapping.Core
{
    using AutoMapper;

    public interface IMapTo<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(T));
    }
}