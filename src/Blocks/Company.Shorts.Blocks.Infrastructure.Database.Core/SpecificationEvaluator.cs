namespace Company.Shorts.Blocks.Infrastructure.Database.Core
{
    using Company.Shorts.Blocks.Application.Contracts;
    using Microsoft.EntityFrameworkCore;

    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> inputQuery, ISpecification<TEntity>? specification = null)
            where TEntity : class
        {
            IQueryable<TEntity> query = inputQuery;

            if (specification is null)
            {
                return query;
            }

            if (specification.Criteria is not null)
            {
                query = query.Where(specification.Criteria);
            }

            query = specification.Includes.Aggregate(
                query,
                (current, include) => current.Include(include));

            query = specification.IncludeStrings.Aggregate(
                query,
                (current, include) => current.Include(include));

            if (specification.OrderBy is not null)
            {
                query = query.OrderBy(specification.OrderBy);
            }

            if (specification.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.GroupBy is not null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(group => group);
            }

            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }

            return query;
        }
    }
}