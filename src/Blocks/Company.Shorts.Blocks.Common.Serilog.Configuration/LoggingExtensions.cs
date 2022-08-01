namespace Company.Shorts.Blocks.Common.Serilog.Configuration
{
    using global::Serilog;
    using global::Serilog.Configuration;

    internal static class LoggingExtensions
    {
        public static LoggerConfiguration WithEscapedExceptionMessage(this LoggerEnrichmentConfiguration enrich)
        {
            return enrich.With<EscapedExceptionMessageEnricher>();
        }
    }
}