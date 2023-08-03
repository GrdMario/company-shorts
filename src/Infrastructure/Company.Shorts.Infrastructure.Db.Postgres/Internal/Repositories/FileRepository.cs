namespace Company.Shorts.Infrastructure.Db.Postgres.Internal.Repositories
{
    using Company.Shorts.Application.Contracts.Db;
    using Company.Shorts.Domain;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class FileRepository : IFileRepository
    {

        private readonly DbSet<File> files;

        public FileRepository(PostgresDbContext dbContext)
        {
            files = dbContext.Set<File>();
        }

        public void Add(File file)
        {
            files.Add(file);
        }

        public async Task<List<File>> GetAsync(int skip, int take, CancellationToken cancellation)
        {
            return await this.files.Skip(skip).Take(take).OrderBy(ob => ob.CreatedAt).ToListAsync(cancellation);
        }

        public Task<File?> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            return files.Where(s => s.Id == id).FirstOrDefaultAsync(s => s.Id == id, cancellation);
        }

        public async Task<File> GetbyIdSafeAsync(Guid id, CancellationToken cancellation)
        {
            return await this.GetByIdAsync(id, cancellation) ?? throw new ApplicationException("Unable to find file.");
        }
    }
}
