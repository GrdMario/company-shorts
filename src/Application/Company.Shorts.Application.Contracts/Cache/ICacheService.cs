namespace Company.Shorts.Application.Contracts.Cache
{
    public interface ICacheService
    {
        Task<T> GetOrAdd<T>(string key, Func<Task<T>> fun);
    }
}
