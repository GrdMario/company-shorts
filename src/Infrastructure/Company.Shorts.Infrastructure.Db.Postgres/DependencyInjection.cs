namespace Company.Shorts.Infrastructure.Db.Postgres
{
    using Company.Shorts.Application.Contracts.Db;
    using Company.Shorts.Infrastructure.Db.Postgres.Internal;
    using Company.Shorts.Infrastructure.Db.Postgres.Internal.Repositories;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    public static class DependencyInjection
    {
        public static IServiceCollection AddPostgresDatabaseLayer(this IServiceCollection services, PostgresAdapterSettings settings)
        {
            services.AddDbContext<PostgresDbContext>(options =>
            {
                options.UseNpgsql(settings.Url);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFileRepository, FileRepository>();

            return services;
        }

        public static IApplicationBuilder MigratePostgresDb(this IApplicationBuilder builder)
        {
            using var scope = builder.ApplicationServices.CreateScope();

            using var dbContext = scope.ServiceProvider.GetService<PostgresDbContext>();

            if (dbContext is null)
            {
                throw new ApplicationException(nameof(dbContext));
            }

            if (dbContext.Database.GetPendingMigrations().Any())
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
