namespace Company.Shorts.Blocks.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToIso86601String(this DateTime dateTime) => Iso801DateTimeFormatOf(dateTime);

        public static string ToIso8601String(this DateTimeOffset dateTime) => Iso801DateTimeFormatOf(dateTime);

        private static string Iso801DateTimeFormatOf(DateTimeOffset dateTime) => dateTime.ToString("o");
    }
}