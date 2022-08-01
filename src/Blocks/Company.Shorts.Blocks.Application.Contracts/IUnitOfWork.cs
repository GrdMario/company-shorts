namespace Company.Shorts.Blocks.Application.Contracts
{ 
    public interface IUnitOfWork : IDisposable
    {
        IDatabaseTransaction? CurrentTransaction { get; }

        bool HasActiveTransaction { get; }

        IDatabaseTransaction BeginTransaction();

        Task<IDatabaseTransaction> BeginTransactionAsync(CancellationToken cancellationToken);

        void Commit();

        Task CommitAsync(CancellationToken cancellationToken = default);

        IRepository<TEntity> Repository<TEntity>() where TEntity : class;

        int Save();

        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}