namespace Company.Shorts.Application.CarAggregate.Query
{
    using AutoMapper;
    using Company.Shorts.Application.CarAggregate.Common;
    using Company.Shorts.Application.Contracts.Http;
    using Company.Shorts.Domain;
    using FluentValidation;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetCarsQuery : IRequest<List<CarResponse>>
    {
    }

    internal sealed class GetCarsQueryValidator : AbstractValidator<GetCarsQuery>
    {
        public GetCarsQueryValidator()
        {
            // Add validation as necessary.
        }
    }

    internal sealed class GetCarsQueryHandler : IRequestHandler<GetCarsQuery, List<CarResponse>>
    {
        private readonly IMapper mapper;
        private readonly ICompanyCarsAdapter companyCarsAdapter;

        public GetCarsQueryHandler(IMapper mapper, ICompanyCarsAdapter companyCarsAdapter)
        {
            this.mapper = mapper;
            this.companyCarsAdapter = companyCarsAdapter;
        }

        public async Task<List<CarResponse>> Handle(GetCarsQuery request, CancellationToken cancellationToken)
        {
            List<Car> cars = await this.companyCarsAdapter.GetAsync(cancellationToken);

            return this.mapper.Map<List<CarResponse>>(cars);
        }
    }
}
