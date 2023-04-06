namespace Company.Shorts.Application.Contracts.Cache
{
    using System.Threading.Tasks;

    public interface ICacheService
    {
        T Get<T>(string key);

        T Add<T>(string key, T item);

        T GetOrAdd<T>(string key, T item);
    }
}
