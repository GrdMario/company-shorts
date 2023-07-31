namespace Company.Shorts.Application.Contracts.Db
{
    using Company.Shorts.Domain;

    public interface IFileRepository
    {
        void Add(File file);

        Task<File> GetbyIdSafeAsync(Guid id, CancellationToken cancellation);

        Task<File?> GetByIdAsync(Guid id, CancellationToken cancellation);
    }
}
