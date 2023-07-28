namespace Company.Shorts.EndToEnd.Tests.Internal
{
    public interface IEnviromentVariableManager
    {
        string Get();

        void Set(string connectionString);
    }
}