namespace Company.Shorts.Integration.Tests.Internal
{
    public interface IFileManager
    {
        T Read<T>(string path);

        string Read(string path);
    }
}