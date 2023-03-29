namespace Company.Graphql.Application.Contracts.Db
{
    public interface IUnitOfWork
    {
        Task SaveAsync(CancellationToken cancellationToken);

        public IUserRepository Users { get; }
    }
}
