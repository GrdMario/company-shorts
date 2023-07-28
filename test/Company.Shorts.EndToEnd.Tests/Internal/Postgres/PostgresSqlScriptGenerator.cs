namespace Company.Shorts.EndToEnd.Tests.Internal.Postgres
{
    using Company.Shorts.EndToEnd.Tests.Internal;
    using Company.Shorts.EndToEnd.Tests.Internal.Common;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class PostgresSqlScriptGenerator : ISqlScriptGenerator
    {
        public string GenerateInsertScript(GenerationContext context)
        {
            var builder = new StringBuilder();

            foreach (var key in context.Database.Keys)
            {
                var item = context.Database[key];

                GenerateColumnsSyntax(builder, key, item);

                var rows = item.Cells.GroupBy(b => b.ParentPath).ToList();

                GenerateValuesSyntax(builder, rows);
            }

            return builder.ToString();

            void GenerateColumnsSyntax(StringBuilder builder, string key, TableContext item)
            {
                builder.AppendLine($"INSERT INTO {key}");
                builder.AppendLine("(");

                var columnNames = string.Join(",\n", item.Columns.Select(s => $"\t\"{s}\""));

                builder.AppendLine(columnNames);

                builder.AppendLine(")");
                builder.AppendLine("VALUES");
            }

            void GenerateValuesSyntax(StringBuilder builder, List<IGrouping<string, Cell>> rows)
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    var value = string.Join(",\n\t", rows[i].Select(s => ParseCellValue(s)).ToList());

                    builder.AppendLine("(");
                    builder.AppendLine($"\t{value}");
                    builder.AppendLine(")");

                    if (i + 1 != rows.Count)
                    {
                        builder.AppendLine(",");
                    }
                }
            }

            object ParseCellValue(Cell cell)
            {
                var value = cell.Value;

                if (cell.Type == JTokenType.Float)
                {
                    value = $"'{cell.Value}'".Replace(',', '.');
                }

                if (cell.Type == JTokenType.String || cell.Type == JTokenType.Date || cell.Type == JTokenType.Guid)
                {
                    value = $"'{cell.Value}'";
                }

                return value;
            }
        }

        public string GenerateDeleteScript(GenerationContext context)
        {
            var builder = new StringBuilder();

            foreach (var key in context.Database.Keys)
            {
                builder.AppendLine($"DELETE FROM {key};");
            }

            return builder.ToString();
        }
    }
}