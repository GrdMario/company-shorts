namespace Company.Shorts.Blocks.Application.Contracts
{
    public interface IHttpContextAccessorAdapter
    {
        T? GetHeaderValue<T>(string key);
        T GetRequiredHeaderValue<T>(string key);

        List<T> GetHeaderValues<T>(string key);
    }
}