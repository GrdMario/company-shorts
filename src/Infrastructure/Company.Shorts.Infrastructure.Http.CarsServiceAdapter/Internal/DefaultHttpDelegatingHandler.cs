namespace Company.Shorts.Infrastructure.Http.CarsServiceAdapter.Internal
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    internal class DefaultHttpDelegatingHandler : DelegatingHandler
    {
        private readonly ILogger<DefaultHttpDelegatingHandler> _logger;

        public DefaultHttpDelegatingHandler(ILogger<DefaultHttpDelegatingHandler> logger)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Sending {RequestMethod} request towards {Request}",
                request.Method,
                request.RequestUri?.ToString());

            var stopwatch = Stopwatch.StartNew();

            HttpResponseMessage response = default!;

            try
            {
                response = await base.SendAsync(request, cancellationToken);

                stopwatch.Stop();

                _logger.LogInformation(
                    "Response took {ElapsedMiliseconds}ms {StatusCode}",
                    stopwatch.ElapsedMilliseconds,
                    response.StatusCode);

                response.EnsureSuccessStatusCode();

                return response;
            }
            catch (Exception exception)
            when (exception is TaskCanceledException or TimeoutException)
            {
                stopwatch.Stop();

                _logger.LogError(
                    "Timeout durning {RequestMethod} to {RequestUri} after {ElapsedMiliseconds}ms {StatusCode}",
                    request.Method,
                    request.RequestUri?.ToString(),
                    stopwatch.ElapsedMilliseconds,
                    response.StatusCode);

                throw;
            }
            catch (Exception exception)
            {
                stopwatch.Stop();

                var message = await response.Content.ReadAsStringAsync(cancellationToken);

                if (!string.IsNullOrEmpty(message))
                {
                    message = $"Remote server replied with message: '{message}'";
                }

                _logger.LogError(
                    exception,
                    "Exception durning {RequestMethod} to {RequestUri} after {ElapsedMiliseconds}ms {StatusCode}.{Message}",
                    request.Method,
                    request.RequestUri?.ToString(),
                    stopwatch.ElapsedMilliseconds,
                    response.StatusCode,
                    message);

                throw;
            }
        }
    }
}
