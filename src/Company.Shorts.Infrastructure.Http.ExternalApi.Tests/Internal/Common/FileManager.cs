namespace Company.Shorts.Infrastructure.Http.ExternalApi.Tests.Internal.Common
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Reflection;
    using Company.Shorts.Infrastructure.Http.ExternalApi.Tests.Internal;

    internal sealed class FileManager : IFileManager
    {
        public T Read<T>(string path)
        {
            string json = this.Read(path);

            var result = JsonConvert.DeserializeObject<T>(json);

            return result ?? throw new ApplicationException(nameof(result));
        }

        public string Read(string path)
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (directory is null)
            {
                throw new ApplicationException(nameof(directory));
            }

            using StreamReader reader = new($"{directory}/{path}");

            string json = reader.ReadToEnd();

            return json;
        }
    }
}