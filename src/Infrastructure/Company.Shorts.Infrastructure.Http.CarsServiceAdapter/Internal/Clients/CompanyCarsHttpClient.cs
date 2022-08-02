namespace Company.Shorts.Infrastucture.Http.CarsServiceAdapter.Internal.Clients
{
    using Company.Shorts.Infrastructure.Http.CarsServiceAdapter.Internal.Clients;
    using Company.Shorts.Infrastucture.Http.CarsServiceAdapter.Internal.Models;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class CompanyCarsHttpClient : HttpClientBase, ICompanyCarsHttpClient
    {
        private const string ApplicationJson = "application/json";

        public CompanyCarsHttpClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task CreateAsync(CreateCarDto createCarDto, CancellationToken cancellationToken)
        {
            string json = JsonSerializer.Serialize(createCarDto);

            HttpRequestMessage message = new(HttpMethod.Post, Endpoints.Post);

            message.Content = new StringContent(json, Encoding.UTF8, ApplicationJson);

            await this.HttpClient.SendAsync(message, cancellationToken);
        }

        public async Task<IEnumerable<CarDto>> GetAsync(CancellationToken cancellationToken)
        {
            HttpRequestMessage message = new(HttpMethod.Get, Endpoints.Get);

            return await this.SendAsync<List<CarDto>>(message, cancellationToken) ?? new();
        }
    }
}
