namespace Company.Shorts.Application.PetsAggregate.Queries
{
    using Company.Shorts.Application.Contracts.Http;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public record PetResponse(int Id, string Name, string Tag);

    public class GetPetsQuery : IRequest<List<PetResponse>>
    {
    }

    public class GetPetsQueryHandler : IRequestHandler<GetPetsQuery, List<PetResponse>>
    {
        private readonly IExternalApi externalApi;

        public GetPetsQueryHandler(IExternalApi externalApi)
        {
            this.externalApi = externalApi;
        }

        public async Task<List<PetResponse>> Handle(GetPetsQuery request, CancellationToken cancellationToken)
        {
            var response = await this.externalApi.GetAsync(cancellationToken);

            var result = response.Select(s => new PetResponse(s.Id, s.Name, s.Tag)).ToList();

            return result;
        }
    }
}
