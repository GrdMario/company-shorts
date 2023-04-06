namespace Company.Shorts.Application.Contracts.Cache
{
    public interface ICacheService
    {
        T Get<T>(string key);

        T Add<T>(string key, T item);
    }
}
