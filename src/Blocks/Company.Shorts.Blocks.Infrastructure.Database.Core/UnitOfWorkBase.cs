namespace Company.Shorts.Blocks.Infrastructure.Database.Core
{
    using Company.Shorts.Blocks.Application.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;

    public class UnitOfWorkBase<TDbContext> : IUnitOfWork
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        private IDatabaseTransaction? _currentTransaction;
        private Dictionary<Type, object>? _repositories = new();

        public UnitOfWorkBase(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IDatabaseTransaction? CurrentTransaction => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction is not null;

        public IDatabaseTransaction BeginTransaction()
        {
            EnsureTransactionNotInProgress();

            _currentTransaction = new DatabaseTransaction((IDatabaseTransaction)_dbContext.Database.BeginTransaction());

            return _currentTransaction;
        }

        public async Task<IDatabaseTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            EnsureTransactionNotInProgress();

            _currentTransaction = new DatabaseTransaction((IDatabaseTransaction)await _dbContext.Database.BeginTransactionAsync(cancellationToken));

            return _currentTransaction;
        }

        public void Commit()
        {
            EnsureTransactionInProgress();

            _currentTransaction.Commit();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            EnsureTransactionInProgress();

            await _currentTransaction.CommitAsync(cancellationToken);
        }

        public IRepository<TEntity> Repository<TEntity>()
            where TEntity : class
        {
            _repositories ??= new Dictionary<Type, object>();

            Type type = typeof(TEntity);

            if (_repositories.TryGetValue(type, out var repository))
            {
                return (IRepository<TEntity>)repository;
            }

            IRepository<TEntity> instance = CreateRepository<TEntity>();

            _repositories.Add(type, instance);
            return instance;
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            _dbContext.Dispose();
        }
        private IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class
        {
            Type repositoryType = typeof(DefaultRepository<,>);

            var repositoryInstance = Activator.CreateInstance(
                repositoryType.MakeGenericType(typeof(TDbContext), typeof(TEntity)),
                _dbContext);

            if (repositoryInstance is null)
            {
                throw new InvalidOperationException($"Unable to find repository for {nameof(TEntity)}.");
            }

            return (IRepository<TEntity>)repositoryInstance;
        }

        private void EnsureTransactionNotInProgress()
        {
            if (_currentTransaction is not null)
            {
                throw new InvalidOperationException("Ongoing transaction exists. There can't be two transactions on the same unit of work scope.");
            }
        }

        [MemberNotNull(nameof(_currentTransaction))]
        private void EnsureTransactionInProgress()
        {
            if (_currentTransaction is null)
            {
                throw new InvalidOperationException("There is no ongoing transaction within unit of work scope.");
            }
        }
    }
}