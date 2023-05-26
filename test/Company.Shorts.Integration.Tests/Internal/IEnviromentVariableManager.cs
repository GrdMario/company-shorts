namespace Company.Shorts.Integration.Tests.Internal
{
    public interface IEnviromentVariableManager
    {
        string Get();

        void Set(string connectionString);
    }
}