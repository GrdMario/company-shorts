namespace Company.Shorts.EndToEnd.Tests.Internal
{
    public interface IFileManager
    {
        T Read<T>(string path);

        string Read(string path);
    }
}