namespace Company.Shorts.Infrastructure.Db.Postgres.Internal.Repositories
{
    using Company.Shorts.Application.Contracts.Db;
    using Company.Shorts.Domain;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal sealed class UserRepository : IUserRepository
    {
        private readonly PostgresDbContext _dbContext;

        public UserRepository(PostgresDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(User user)
        {
            this._dbContext.Set<User>().Add(user);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await this._dbContext.Set<User>().FindAsync(new { id }) ?? throw new ArgumentNullException("not found.");
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await this._dbContext.Set<User>().ToListAsync();
        }
    }
}
