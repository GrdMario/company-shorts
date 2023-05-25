namespace Company.Shorts.Application.Contracts.Db
{
    using Company.Shorts.Domain;

    public interface IUserRepository
    {
        void Add(User user);

        Task<User> GetByIdAsync(int id);

        Task<List<User>> GetUsersAsync();
    }
}
