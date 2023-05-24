namespace Company.Graphql.Application.Contracts.Db
{
    using System.Linq;
    using System.Linq.Expressions;

    public interface IQueryRepository<T>
    {
        IQueryable<T> Entities { get; }

        IQueryable<T> Include(Expression<Func<T, object>> includes);
    }
}
