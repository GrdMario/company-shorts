namespace Company.Shorts.Application.Contracts.Db
{
    using Company.Shorts.Domain;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IFileRepository
    {
        void Add(File file);

        Task<File> GetbyIdSafeAsync(Guid id, CancellationToken cancellation);

        Task<File?> GetByIdAsync(Guid id, CancellationToken cancellation);

        Task<List<File>> GetAsync(int skip, int take, CancellationToken cancellation);
    }
}
