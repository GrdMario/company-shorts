namespace Company.Shorts.Infrastructure.Db.Postgres.Internal
{
    using Company.Shorts.Application.Contracts.Db;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly PostgresDbContext dbContext;

        public UnitOfWork(PostgresDbContext dbContext, IUserRepository userRepository)
        {
            this.dbContext = dbContext;
            this.Users = userRepository;
        }

        public IUserRepository Users { get; }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await this.dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
