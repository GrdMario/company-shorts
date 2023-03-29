namespace Company.Shorts.Application.UserAggregate.Command
{
    using Company.Graphql.Application.Contracts.Db;
    using Company.Shorts.Domain;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public record CreateUserCommand(string UserName, string Email, string Address, string ProfilePicture) : IRequest;

    internal sealed class CreateUserCommandHandler : AsyncRequestHandler<CreateUserCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = request.UserName,
                Email = request.Email,
                Address = request.Address,
                ProfilePicture = request.ProfilePicture
            };

            this.unitOfWork.Users.Add(user);

            await this.unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
