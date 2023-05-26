namespace Company.Shorts.Integration.Tests.Internal.Postgres
{
    using Company.Shorts.Integration.Tests.Internal.Common;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal sealed class PostgresSqlGenerationManager : ISqlGenerationManager
    {
        public GenerationItem Generate(Dictionary<string, object[]> obj)
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

                        if (type == JTokenType.String || type == JTokenType.Date || type == JTokenType.Guid)
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

            return new GenerationItem(insert, delete);
        }
    }
}