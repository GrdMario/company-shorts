namespace Company.Shorts.Infrastructure.Db.Postgres.Internal
{
    using Company.Shorts.Application.Contracts.Db;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly PostgresDbContext dbContext;

        public UnitOfWork(PostgresDbContext dbContext, IFileRepository fileRepository)
        {
            this.dbContext = dbContext;
            Files = fileRepository;
        }

        public IFileRepository Files { get; }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
