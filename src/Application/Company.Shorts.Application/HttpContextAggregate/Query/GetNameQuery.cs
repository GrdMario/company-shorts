namespace Company.Shorts.Application.HttpContextAggregate.Query
{
    using Company.Shorts.Application.Contracts.HttpContextAccessor;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetNameQuery : IRequest<string>
    {
    }

    internal sealed class GetNameQueryHandler : IRequestHandler<GetNameQuery, string>
    {
        private readonly IHttpContextAccessorAdapter httpContextAccessorAdapter;

        public GetNameQueryHandler(IHttpContextAccessorAdapter httpContextAccessorAdapter)
        {
            this.httpContextAccessorAdapter = httpContextAccessorAdapter;
        }

        public async Task<string> Handle(GetNameQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(this.httpContextAccessorAdapter.GetName());
        }
    }
}
