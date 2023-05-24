namespace Company.Shorts.Blocks.Common.Serilog.Configuration
{
    using global::Serilog;
    using global::Serilog.AspNetCore;
    using global::Serilog.Events;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;

    public static class ApplicationBuilderExtensins
    {
        public static IApplicationBuilder UseSerilogConfiguration(
            this IApplicationBuilder builder,
            Action<RequestLoggingOptions>? serilogOptions = null)
        {
            serilogOptions ??= options =>
            {
                options.EnrichDiagnosticContext = EnrichFromRequest;
                options.GetLevel = ExcludeHealthChecks;
            };

            builder.UseSerilogRequestLogging(serilogOptions);

            return builder;
        }

        private static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            HttpRequest request = httpContext.Request;

            diagnosticContext.Set(DiagnosticProperties.Host, request.Host);
            diagnosticContext.Set(DiagnosticProperties.Protocol, request.Protocol);
            diagnosticContext.Set(DiagnosticProperties.Scheme, request.Scheme);

            if (request.QueryString.HasValue)
            {
                diagnosticContext.Set(DiagnosticProperties.QueryString, request.QueryString.Value);
            }

            diagnosticContext.Set(DiagnosticProperties.ContentType, request.ContentType);

            Endpoint? endpoint = httpContext.GetEndpoint();

            if (endpoint is not null)
            {
                diagnosticContext.Set(DiagnosticProperties.EndpointName, endpoint.DisplayName);
            }
        }

        private static LogEventLevel ExcludeHealthChecks(
            HttpContext httpContext,
            double _,
            Exception? exception)
        {
            if (IsError(httpContext))
            {
                return LogEventLevel.Error;
            }

            return IsHealthCheckEndpointt(httpContext)
                ? LogEventLevel.Verbose
                : LogEventLevel.Information;
        }

        private static bool IsError(HttpContext httpContext)
            => httpContext.Response.StatusCode >= StatusCodes.Status500InternalServerError;

        private static bool IsHealthCheckEndpointt(HttpContext httpContext)
        {
            Endpoint? endpoint = httpContext.GetEndpoint();

            if (endpoint is null)
            {
                return false;
            }

            return string.Equals(
                endpoint.DisplayName,
                "Health checks",
                StringComparison.Ordinal);
        }
    }
}