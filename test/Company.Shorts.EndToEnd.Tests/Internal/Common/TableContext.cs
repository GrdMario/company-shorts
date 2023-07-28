namespace Company.Shorts.EndToEnd.Tests.Internal.Common
{
    using System.Collections.Generic;

    public sealed class TableContext
    {
        public TableContext(string table, List<string> columns, List<Cell> cells)
        {
            Table = table;
            Columns = columns;
            Cells = cells;
        }

        public string Table { get; set; }

        public List<string> Columns { get; set; }

        public List<Cell> Cells { get; set; }
    }
}