namespace Company.Shorts.Application.UserAggregate.Query
{
    using Company.Graphql.Application.Contracts.Db;
    using Company.Shorts.Domain;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public record GetUsersQuery : IRequest<List<User>>;

    internal sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<User>>
    {
        private readonly IUserRepository userRepository;

        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<List<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await this.userRepository.GetUsersAsync();
        }
    }
}
