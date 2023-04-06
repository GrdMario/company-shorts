namespace Company.Shorts.Application.UserAggregate.Query
{
    using Company.Graphql.Application.Contracts.Db;
    using Company.Shorts.Application.Contracts.Cache;
    using Company.Shorts.Domain;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public record GetUsersQuery : IRequest<List<User>>;

    internal sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<User>>
    {
        private const string Key = "users";

        private readonly ICacheService cacheService;
        private readonly IUserRepository userRepository;

        public GetUsersQueryHandler(IUserRepository userRepository, ICacheService cacheService)
        {
            this.userRepository = userRepository;
            this.cacheService = cacheService;
        }

        public async Task<List<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = this.cacheService.Get<List<User>>(Key);
            
            if (users is null)
            {
                users = await this.userRepository.GetUsersAsync();

                this.cacheService.Add(Key, users);
            }

            return users;
        }
    }
}
