namespace Company.Shorts.Application.Contracts.Http
{
    using Company.Shorts.Domain;

    public interface IExternalApi
    {
        Task<List<Pet>> GetAsync(CancellationToken cancellationToken);
    }
}
