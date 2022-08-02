namespace Company.Shorts.Infrastucture.Http.CarsServiceAdapter.Internal.Clients
{
    using Company.Shorts.Infrastucture.Http.CarsServiceAdapter.Internal.Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal interface ICompanyCarsHttpClient
    {
        internal Task CreateAsync(CreateCarDto createCarDto, CancellationToken cancellationToken);

        internal Task<IEnumerable<CarDto>> GetAsync(CancellationToken cancellationToken);
    }
}
