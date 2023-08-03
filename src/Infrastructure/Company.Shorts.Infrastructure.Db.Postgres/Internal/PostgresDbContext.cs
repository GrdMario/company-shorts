namespace Company.Shorts.Infrastructure.Db.Postgres.Internal
{
    using Company.Shorts.Domain;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    internal sealed class PostgresDbContext : DbContext
    {
        public PostgresDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<File> Files { get; } = default!;
    }
}
