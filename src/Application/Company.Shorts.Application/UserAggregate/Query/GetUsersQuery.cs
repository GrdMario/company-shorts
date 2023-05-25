namespace Company.Shorts.Application.UserAggregate.Query
{
    using Company.Shorts.Application.Contracts.Db;
    using Company.Shorts.Domain;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public record GetUsersQuery : IRequest<List<User>>;

    internal sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<User>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetUsersQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await this.unitOfWork.Users.GetUsersAsync();
        }
    }
}
