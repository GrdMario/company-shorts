namespace Company.Shorts.Application.CarAggregate.Command
{
    using Company.Shorts.Application.Contracts.Http;
    using Company.Shorts.Blocks.Application.Contracts;
    using Company.Shorts.Domain;
    using FluentValidation;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateCarCommand : IRequest
    {
        public string Name { get; set; } = default!;

        public int Consumption { get; set; }

        public int NumberOfCylinders { get; set; }

        public int HorsePower { get; set; }

        public int Weight { get; set; }

        public int Acceleration { get; set; }

        public DateTimeOffset Year { get; set; }

        public string Origin { get; set; } = default!;
    }

    internal sealed class CreateCarCommandValidtor : AbstractValidator<CreateCarCommand>
    {
        public CreateCarCommandValidtor()
        {
            RuleFor(c => c.Name).NotEmpty();
            // Other validation rules.
        }
    }

    internal sealed class CreateCarCommandHandler : AsyncRequestHandler<CreateCarCommand>
    {
        private readonly ICompanyCarsAdapter companyCarsAdapter;

        public CreateCarCommandHandler(ICompanyCarsAdapter companyCarsAdapter)
        {
            this.companyCarsAdapter = companyCarsAdapter;
        }

        protected override async Task Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            Car car = new(
                SystemGuid.NewGuid,
                request.Name,
                request.Consumption,
                request.NumberOfCylinders,
                request.HorsePower,
                request.Weight,
                request.Acceleration,
                request.Year,
                request.Origin);

            await this.companyCarsAdapter.CreateAsync(car, cancellationToken);
        }
    }
}
