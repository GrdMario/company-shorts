namespace Company.Shorts.Infrastucture.Http.CarsServiceAdapter.Internal
{
    using AutoMapper;
    using Company.Shorts.Application.Contracts.Http;
    using Company.Shorts.Domain;
    using Company.Shorts.Infrastucture.Http.CarsServiceAdapter.Internal.Clients;
    using Company.Shorts.Infrastucture.Http.CarsServiceAdapter.Internal.Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class CompanyCarsAdapter : ICompanyCarsAdapter
    {
        private readonly IMapper mapper;
        private readonly ICompanyCarsHttpClient companyCarsHttpClient;

        public CompanyCarsAdapter(IMapper mapper, ICompanyCarsHttpClient companyCarsHttpClient)
        {
            this.mapper = mapper;
            this.companyCarsHttpClient = companyCarsHttpClient;
        }

        public async Task CreateAsync(Car car, CancellationToken cancellationToken)
        {
            var dto = this.mapper.Map<CreateCarDto>(car);

            await this.companyCarsHttpClient.CreateAsync(dto, cancellationToken);
        }

        public async Task<List<Car>> GetAsync(CancellationToken cancellationToken)
        {
            var response = await this.companyCarsHttpClient.GetAsync(cancellationToken);

            return this.mapper.Map<List<Car>>(response);
        }
    }
}
