namespace Company.Shorts.Blocks.Common.Serilog.Configuration
{
    internal static class StringExtensions
    {
        public static string EscapeNewLine(this string value, string substitue = "\\r\\n")
        {
            return value.Replace(Environment.NewLine, substitue);
        }
    }
}