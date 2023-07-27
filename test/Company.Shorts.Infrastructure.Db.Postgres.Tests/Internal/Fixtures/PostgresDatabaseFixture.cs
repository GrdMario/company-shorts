namespace Company.Shorts.Integration.Db.Postgres.Internal.Fixtures
{
    using Company.Shorts.Infrastructure.Db.Postgres.Internal;
    using Company.Shorts.Integration.Db.Postgres.Internal.Postgres;
    using DotNet.Testcontainers.Builders;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Data;
    using Testcontainers.PostgreSql;

    public class PostgresDatabaseFixture : IDisposable
    {
        private bool _disposed;
        private readonly IEnviromentVariableManager eventVariableManager = new PostgresEnviromentVariableManager();

        public PostgresDatabaseFixture()
        {
            this.PqsqlDatabase = new PostgreSqlBuilder()
                .WithImage(PostgreSqlContainerConstants.Image)
                .WithDatabase(PostgreSqlContainerConstants.Database)
                .WithUsername(PostgreSqlContainerConstants.Username)
                .WithPassword(PostgreSqlContainerConstants.Password)
                .WithExposedPort(PostgreSqlContainerConstants.Port)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(PostgreSqlContainerConstants.Port))
                .Build();

            this.PqsqlDatabase.StartAsync().Wait();

            var options = new DbContextOptionsBuilder<PostgresDbContext>()
                .UseNpgsql(this.PqsqlDatabase.GetConnectionString())
                .Options;

            this.DbContext = new PostgresDbContext(options);

            this.DbContext.Database.Migrate();

            this.eventVariableManager.Set(PqsqlDatabase.GetConnectionString());
        }

        internal PostgresDbContext DbContext { get; }

        public PostgreSqlContainer PqsqlDatabase { get; }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    PqsqlDatabase.DisposeAsync().AsTask().Wait();
                    this.DbContext.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}