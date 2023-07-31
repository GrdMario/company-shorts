namespace Company.Shorts.Infrastructure.Db.Postgres.Internal.Repositories
{
    using Company.Shorts.Application.Contracts.Db;
    using Company.Shorts.Domain;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class FileRepository : IFileRepository
    {

        private readonly DbSet<File> files;

        public FileRepository(PostgresDbContext dbContext)
        {
            this.files = dbContext.Set<File>();
        }

        public void Add(File file)
        {
            this.files.Add(file);
        }

        public Task<File?> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            return this.files.FirstOrDefaultAsync(s => s.Id == id, cancellation);
        }

        public Task<File> GetbyIdSafeAsync(Guid id, CancellationToken cancellation)
        {
            return this.GetbyIdSafeAsync(id, cancellation) ?? throw new ApplicationException("Unable to find file.");
        }
    }
}
