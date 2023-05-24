namespace Company.Shorts.Application.ExampleAggregate.Command
{
    using Company.Shorts.Application.Contracts.Http;
    using Company.Shorts.Domain;
    using FluentValidation;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public record CreateExampleCommand(string Name) : IRequest;

    public class CreateExampleCommandValidator : AbstractValidator<CreateExampleCommand>
    {
        public CreateExampleCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }

    internal sealed class CreateExampleCommandHandler : IRequestHandler<CreateExampleCommand>
    {
        private readonly IExampleAdapter exampleAdapter;

        public CreateExampleCommandHandler(IExampleAdapter exampleAdapter)
        {
            this.exampleAdapter = exampleAdapter;
        }

        public async Task Handle(CreateExampleCommand request, CancellationToken cancellationToken)
        {
            Example example = new(Guid.NewGuid(), request.Name);

            await this.exampleAdapter.CreateAsync(example, cancellationToken);
        }
    }
}
