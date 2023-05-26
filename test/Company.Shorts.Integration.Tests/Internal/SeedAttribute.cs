namespace Company.Shorts.Integration.Tests.Internal
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Xunit.Sdk;

    public record SqlItem(string Insert, string Delete);

    public static class SqlGenerationUtility
    {
        public static SqlItem Generate(Dictionary<string, object[]> obj)
        {
            var builder = new StringBuilder();

            foreach (var key in obj.Keys)
            {
                var table = obj[key];

                foreach (var row in table)
                {
                    builder.AppendLine($"INSERT INTO {key}");

                    var rowData = JToken.FromObject(row);

                    builder.AppendLine("(");

                    var iterable = rowData.ToList();

                    for (int i = 0; i < iterable.Count; i++)
                    {
                        var value = $"\"{iterable[i].Path}\"";

                        if (i + 1 != rowData.ToList().Count)
                        {
                            value += ",";
                        }

                        builder.AppendLine($"\t{value}");
                    }

                    builder.AppendLine(")");
                    builder.AppendLine("VALUES");
                    builder.AppendLine("(");

                    for (int i = 0; i < iterable.Count; i++)
                    {
                        var item = iterable[i];

                        var type = item.Values().First().Type;

                        var value = $"{iterable[i].First}";

                        if (type == JTokenType.Float)
                        {
                            value = $"'{iterable[i].First}'".Replace(',', '.');
                        }

                        if (type == JTokenType.String || type == JTokenType.Date || type== JTokenType.Guid)
                        {
                            value = $"'{iterable[i].First}'";
                        }

                        if (i + 1 != rowData.ToList().Count)
                        {
                            value += ",";
                        }

                        builder.AppendLine($"\t{value}");
                    }

                    builder.AppendLine(");");
                }
            }

            var insert = builder.ToString();
            builder.Clear();

            foreach (var key in obj.Keys)
            {
                builder.AppendLine($"DELETE FROM {key};");
            }

            var delete = builder.ToString();

            builder.Clear();

            return new SqlItem(insert, delete);
        }


    }

    public class SeedAttribute : BeforeAfterTestAttribute
    {
        private SqlItem sqlItem = default!;

        public SeedAttribute(string filePath)
        {
            FilePath = filePath;
        }

        public string FilePath { get; }

        public override void After(MethodInfo methodUnderTest)
        {
            DbConnectionUtility.Execute(this.sqlItem.Delete);

            base.After(methodUnderTest);
        }

        public override void Before(MethodInfo methodUnderTest)
        {
            var attribute = methodUnderTest.GetCustomAttribute<SeedAttribute>() ?? throw new ArgumentException(nameof(methodUnderTest));

            var obj = FileUtils.Read(attribute.FilePath);

            this.sqlItem = SqlGenerationUtility.Generate(obj);

            DbConnectionUtility.Execute(this.sqlItem.Insert);

            base.Before(methodUnderTest);
        }
    }
}