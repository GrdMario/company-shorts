namespace Company.Shorts.Application.ExampleAggregate.Command
{
    using FluentValidation;
    using MediatR;
    using Company.Shorts.Application.Contracts.Http;
    using Company.Shorts.Domain;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateExampleCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;
    }

    internal sealed class UpdateExampleCommandValidator : AbstractValidator<UpdateExampleCommand>
    {
        public UpdateExampleCommandValidator()
        {
            // TODO: Add validation.
        }
    }

    internal sealed class UpdateExampleCommandHandler : IRequestHandler<UpdateExampleCommand>
    {
        private readonly IExampleAdapter _exampleAdapter;

        public UpdateExampleCommandHandler(IExampleAdapter exampleAdapter)
        {
            _exampleAdapter = exampleAdapter;
        }

        public async Task Handle(UpdateExampleCommand request, CancellationToken cancellationToken)
        {
            Example example = await _exampleAdapter.GetByIdAsync(request.Id, cancellationToken);

            // TODO: Domain/business logic.

            await _exampleAdapter.UpdateAsync(example, cancellationToken);
        }
    }
}
