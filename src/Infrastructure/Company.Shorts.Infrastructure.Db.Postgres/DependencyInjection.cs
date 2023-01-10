namespace Company.Shorts.Infrastructure.Db.Postgres
{
    using Company.Shorts.Infrastructure.Db.Postgres.Internal;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddPostgresDatabaseLayer(this IServiceCollection services, PostgresAdapterSettings settings)
        {
            services.AddDbContext<PostgresDbContext>(options =>
            {
                options.UseNpgsql(settings.Url);
            }, ServiceLifetime.Transient);

            DatabaseDependencyInjection.AddRepositories<PostgresDbContext>(services);

            return services;
        }

        public static IApplicationBuilder MigratePostgresDb(this IApplicationBuilder builder)
        {
            using var scope = builder.ApplicationServices.CreateScope();

            using var dbContext = scope.ServiceProvider.GetService<PostgresDbContext>();

            if (dbContext is null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (dbContext.Database.GetPendingMigrations().Count() > 0)
            {
                dbContext.Database.Migrate();
            }

            return builder;
        }
    }

    public class PostgresAdapterSettings
    {
        public const string Key = nameof(PostgresAdapterSettings);

        public string Url { get; set; } = default!;
    }
}
