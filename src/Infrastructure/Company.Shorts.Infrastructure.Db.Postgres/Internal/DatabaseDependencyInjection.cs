namespace Company.Shorts.Infrastructure.Db.Postgres.Internal
{
    using Company.Graphql.Application.Contracts.Db;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using System.Reflection;

    public static class DatabaseDependencyInjection
    {
        public static void AddRepositories<T>(IServiceCollection services) where T : DbContext
        {
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                Type setType = property.PropertyType;
                var isDbSet = setType.IsGenericType && typeof(DbSet<>).IsAssignableFrom(setType.GetGenericTypeDefinition());

                if (!isDbSet)
                {
                    continue;
                }

                Type entityType = property.PropertyType.GetGenericArguments().Single();

                Type repositoryType = typeof(IQueryRepository<>);
                Type entityRepositoryType = repositoryType.MakeGenericType(entityType);

                Type implementationType = typeof(QueryRepository<,>);
                Type entityImplementationType = implementationType.MakeGenericType(typeof(T), entityType);

                services.AddTransient(entityRepositoryType, entityImplementationType);
            }
        }
    }
}