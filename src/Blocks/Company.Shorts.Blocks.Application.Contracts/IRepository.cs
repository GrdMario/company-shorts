namespace Company.Shorts.Blocks.Application.Contracts
{
    using System.Linq.Expressions;

    public interface IRepository<TEntity>
        where TEntity : class
    {
        TEntity? Find(object id);

        Task<TEntity?> FindAsync(object id, CancellationToken cancellationToken);

        List<TEntity> Find(ISpecification<TEntity>? specification = null);

        Task<List<TEntity>> FindAsync(CancellationToken cancellationToken);

        Task<List<TEntity>> FindAsync(ISpecification<TEntity>? specification, CancellationToken cancellationToken);

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        bool Contains(ISpecification<TEntity>? specification = null);

        bool Contains(Expression<Func<TEntity, bool>> predicate);

        int Count(ISpecification<TEntity>? specification = null);

        int Count(Expression<Func<TEntity, bool>> predicate);
    }
}