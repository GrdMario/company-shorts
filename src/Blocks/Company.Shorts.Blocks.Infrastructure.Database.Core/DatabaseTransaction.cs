namespace Company.Shorts.Blocks.Infrastructure.Database.Core
{
    using Company.Shorts.Blocks.Application.Contracts;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class DatabaseTransaction : IDatabaseTransaction
    {
        private readonly IDatabaseTransaction _transaction;

        public DatabaseTransaction(IDatabaseTransaction transaction)
        {
            _transaction = transaction;
        }

        public Guid Id => _transaction.Id;

        public void Commit()
        {
            _transaction.Commit();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _transaction.CommitAsync(cancellationToken);
        }
    }
}