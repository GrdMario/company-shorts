namespace Company.Shorts.Infrastructure.Db.Postgres.Internal
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    internal sealed class PostgresDbContextFactory : IDesignTimeDbContextFactory<PostgresDbContext>
    {
        public PostgresDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = BuildConfiguration(args);

            var optionsBuilder = new DbContextOptionsBuilder<PostgresDbContext>();
            var connectionString = configuration.GetSection(args[0]).Value;

            optionsBuilder.UseNpgsql(connectionString);

            PostgresDbContext instance = new(optionsBuilder.Options);

            if (instance is null)
            {
                throw new InvalidOperationException($"Unable to initialize {nameof(PostgresDbContext)} instance.");
            }

            return instance;
        }

        private static IConfigurationRoot BuildConfiguration(string[] args)
        {
            return new ConfigurationBuilder()
                .SetBasePath(args[2])
                .AddJsonFile(
                    path: "appsettings.json",
                    optional: false,
                    reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddUserSecrets(args[1])
                .AddCommandLine(args)
                .Build();
        }
    }
}

