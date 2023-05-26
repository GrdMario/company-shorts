namespace Company.Shorts.Integration.Tests.Internal.Common
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    internal sealed class FileManager : IFileManager
    {
        public Dictionary<string, object[]> Read(string path)
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            var parsedDirectory = directory.Replace(@"\bin\Debug\net7.0", "");

            using StreamReader reader = new StreamReader($"{parsedDirectory}/{path}");

            string json = reader.ReadToEnd();

            var result = JsonConvert.DeserializeObject<Dictionary<string, object[]>>(json);

            return result ?? throw new ArgumentNullException(nameof(result));
        }
    }
}