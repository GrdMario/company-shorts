namespace Company.Shorts.Integration.Tests.Internal
{
    using System.Collections.Generic;

    public interface IFileManager
    {
        Dictionary<string, object[]> Read(string path);
    }
}