namespace Company.Shorts.Infrastructure.Db.Postgres.Internal
{
    using Company.Graphql.Application.Contracts.Db;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Linq.Expressions;

    public class QueryRepository<TDbContext, TEntity> : IQueryRepository<TEntity>
        where TEntity : class
        where TDbContext : DbContext
    {
        private readonly TDbContext dbContext;

        public QueryRepository(TDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<TEntity> Entities => this.dbContext.Set<TEntity>().AsNoTracking();

        public IQueryable<TEntity> Include(Expression<Func<TEntity, object>> includes)
            => this.dbContext.Set<TEntity>().Include(includes).AsNoTracking();
    }
}