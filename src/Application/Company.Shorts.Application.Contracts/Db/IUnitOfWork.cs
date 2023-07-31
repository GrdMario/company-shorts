namespace Company.Shorts.Application.Contracts.Db
{
    public interface IUnitOfWork
    {
        Task SaveAsync(CancellationToken cancellationToken);

        public IFileRepository Files { get; }
    }
}
