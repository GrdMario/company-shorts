namespace Company.Shorts.Infrastructure.Http.ExternalApi.Tests.Internal
{
    public interface IFileManager
    {
        T Read<T>(string path);

        string Read(string path);
    }
}