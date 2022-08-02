namespace Company.Shorts.Infrastructure.Http.CarsServiceAdapter.Internal.Clients
{
    using System.Text.Json;
    using System.Threading.Tasks;

    public abstract class HttpClientBase
    {
        protected HttpClientBase(HttpClient httpClient)
        {
            this.HttpClient = httpClient;
        }

        protected HttpClient HttpClient { get; }

        protected async Task<T?> SendAsync<T>(HttpRequestMessage message, CancellationToken cancellationToken)
            where T : class
        {
            var response = await this.HttpClient.SendAsync(message, cancellationToken);

            var json = await response.Content.ReadAsStringAsync(cancellationToken);

            return string.IsNullOrEmpty(json)
                ? null
                : JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        protected async Task SendAsync(HttpRequestMessage message, CancellationToken cancellationToken)
        {
            await this.HttpClient.SendAsync(message, cancellationToken);
        }
    }
}
