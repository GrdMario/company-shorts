namespace Company.Shorts.Blocks.Common.Serilog.Configuration
{
    using global::Serilog.Core;
    using global::Serilog.Events;

    internal sealed class EscapedExceptionMessageEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent.Exception is null)
            {
                return;
            }

            LogEventProperty property =
                propertyFactory.CreateProperty(
                    name: "_Exception",
                    value: logEvent.Exception.ToString().EscapeNewLine());

            logEvent.AddPropertyIfAbsent(property);
        }
    }
}