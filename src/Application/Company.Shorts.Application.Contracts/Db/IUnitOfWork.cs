namespace Company.Shorts.Application.Contracts.Db
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        Task SaveAsync(CancellationToken cancellationToken);

        public IFileRepository Files { get; }
    }
}
