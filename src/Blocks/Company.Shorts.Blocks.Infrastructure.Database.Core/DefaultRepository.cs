namespace Company.Shorts.Blocks.Infrastructure.Database.Core
{
    using Company.Shorts.Blocks.Application.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    public class DefaultRepository<TDbContext, TEntity> : IRepository<TEntity>
        where TDbContext : DbContext
        where TEntity : class
    {
        private readonly TDbContext _context;

        public DefaultRepository(TDbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public bool Contains(ISpecification<TEntity>? specification = null)
        {
            return Count(specification) > 0;
        }

        public bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return Count(predicate) > 0;
        }

        public int Count(ISpecification<TEntity>? specification = null)
        {
            return ApplySpecification(specification).Count();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Count(predicate);
        }

        public TEntity? Find(object id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public List<TEntity> Find(ISpecification<TEntity>? specification = null)
        {
            return ApplySpecification(specification).ToList();
        }

        public async Task<TEntity?> FindAsync(object id, CancellationToken cancellationToken)
        {
            return await _context.Set<TEntity>().FindAsync(new[] { id }, cancellationToken);
        }

        public async Task<List<TEntity>> FindAsync(CancellationToken cancellationToken)
        {
            return await ApplySpecification().ToListAsync(cancellationToken);
        }

        public async Task<List<TEntity>> FindAsync(ISpecification<TEntity>? specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification).ToListAsync(cancellationToken);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity>? specification = null)
        {
            return SpecificationEvaluator.GetQuery<TEntity>(_context.Set<TEntity>(), specification);
        }
    }
}