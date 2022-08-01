namespace Company.Shorts.Application.Contracts.Http
{
    using Company.Shorts.Domain;

    public interface IExampleAdapter
    {
        Task<Example> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<List<Example>> GetAsync(CancellationToken cancellationToken);

        Task CreateAsync(Example example, CancellationToken cancellationToken);

        Task UpdateAsync(Example example, CancellationToken cancellationToken);
    }
}
