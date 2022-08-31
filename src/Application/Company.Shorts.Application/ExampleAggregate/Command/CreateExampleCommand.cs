namespace Company.Shorts.Application.ExampleAggregate.Command
{
    using Company.Shorts.Application.Contracts.Http;
    using Company.Shorts.Domain;
    using FluentValidation;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public record CreateExampleCommand(string Name, bool ThrowCustomException, bool ThrowNotFoundException, bool ThorwInternalException) : IRequest;

    public class CreateExampleCommandValidator : AbstractValidator<CreateExampleCommand>
    {
        public CreateExampleCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }

    internal sealed class CreateExampleCommandHandler : AsyncRequestHandler<CreateExampleCommand>
    {
        private readonly IExampleAdapter exampleAdapter;

        public CreateExampleCommandHandler(IExampleAdapter exampleAdapter)
        {
            this.exampleAdapter = exampleAdapter;
        }

        protected override async Task Handle(CreateExampleCommand request, CancellationToken cancellationToken)
        {
            if (request.ThrowCustomException)
            {
                throw new Blocks.Common.Exceptions.ValidationException("This is a custom error.");
            }

            if (request.ThrowNotFoundException)
            {
                throw new Blocks.Common.Exceptions.NotFoundException("Resource not found.");
            }

            if (request.ThorwInternalException)
            {
                throw new Exception("Unhandled exception");
            }

            Example example = new(Guid.NewGuid(), request.Name);

            await this.exampleAdapter.CreateAsync(example, cancellationToken);
        }
    }
}
