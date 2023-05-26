namespace Company.Shorts.Integration.Tests.Internal
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    public static class FileUtils
    {
        public static Dictionary<string, object[]> Read(string path)
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