namespace Company.Shorts.Application.Contracts.Http
{
    using Company.Shorts.Domain;

    public interface ICompanyCarsAdapter
    {
        Task CreateAsync(Car car, CancellationToken cancellationToken);

        Task<List<Car>> GetAsync(CancellationToken cancellationToken);
    }
}
