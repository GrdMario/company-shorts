namespace Company.Shorts.Infrastructure.Http.ExternalApi.Tests.Internal
{
    public interface IEnviromentVariableManager
    {
        string Get();

        void Set(string connectionString);
    }
}