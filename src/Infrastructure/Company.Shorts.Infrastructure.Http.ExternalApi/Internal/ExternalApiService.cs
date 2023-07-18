namespace Company.Shorts.Infrastructure.Http.ExternalApi.Internal
{
    using Company.Shorts.Application.Contracts.Http;
    using Company.Shorts.Domain;
    using Company.Shorts.Infrastructure.Http.ExternalApi.Internal.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class ExternalApiService : IExternalApi
    {
        private readonly HttpClient httpClient;

        public ExternalApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<Pet>> GetAsync(CancellationToken cancellationToken)
        {
            var response = await this.httpClient.GetAsync("/pets", cancellationToken);

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync(cancellationToken); 

            var convert = JsonSerializer.Deserialize<List<PetDto>>(responseAsString);

            return convert is null
                ? throw new ApplicationException("Unable to find pet.")
                : convert.Select(s => new Pet(s.Id, s.Name, s.Tag)).ToList();
        }
    }
}