namespace Company.Shorts.Application.ExampleAggregate.Command
{
    using Company.Shorts.Application.Contracts.Http;
    using Company.Shorts.Blocks.Application.Contracts;
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

    internal sealed class CreateExampleCommandHandler : AsyncRequestHandler<CreateExampleCommand>
    {
        private readonly IExampleAdapter _exampleAdapter;
        private readonly ICompanyCarsAdapter _companyCarsAdapter;

        public CreateExampleCommandHandler(
            IExampleAdapter exampleAdapter, ICompanyCarsAdapter companyCarsAdapter)
        {
            _exampleAdapter = exampleAdapter;
            _companyCarsAdapter = companyCarsAdapter;
        }

        protected override async Task Handle(CreateExampleCommand request, CancellationToken cancellationToken)
        {
            Car car = new(Guid.NewGuid(), "BMW", 1, 1, 1, 1, 1, DateTimeOffset.UtcNow, "USA");

            await _companyCarsAdapter.CreateCorrectAsync(car, cancellationToken);

            await _companyCarsAdapter.CreateIncorrectAsync(car, cancellationToken);

            var cars = await _companyCarsAdapter.GetCarsCorrectlyAsync(cancellationToken);
        }
    }
}
