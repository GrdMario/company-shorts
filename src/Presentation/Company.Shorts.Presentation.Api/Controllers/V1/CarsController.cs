namespace Company.Shorts.Presentation.Api.Controllers.V1
{
    using AutoMapper;
    using Company.Shorts.Application.CarAggregate.Common;
    using Company.Shorts.Application.CarAggregate.Query;
    using Company.Shorts.Application.ExampleAggregate.Command;
    using Company.Shorts.Presentation.Api.Controllers.V1.Models.Cars;
    using Company.Shorts.Presentation.Api.Controllers.V1.Models.Examples;
    using Company.Shorts.Presentation.Api.Internal.Constants;
    using Company.Shorts.Presentation.Api.Internal.Examples.V1.Cars;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;
    using Swashbuckle.AspNetCore.Filters;

    [ApiVersion(ApiVersions.V1)]
    public class CarsController : ApiControllerBase
    {
        public CarsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        /// <summary>
        /// Gets cars.
        /// </summary>
        /// <param name="query">Model for quering cars.</param>
        [HttpGet]
        [SwaggerOperation(OperationId = nameof(Get), Tags = new[] { ApiTags.Cars })]
        [SwaggerRequestExample(typeof(GetCarsQueryDto), typeof(GetCarsQueryDtoExample))]
        [ProducesResponseType(typeof(List<CarResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] GetCarsQueryDto request, CancellationToken cancellationToken)
        {
            return await ProcessAsync<GetCarsQueryDto, GetCarsQuery, List<CarResponse>>(request, null, cancellationToken);
        }

        /// <summary>
        /// Creates car.
        /// </summary>
        [HttpPost]
        [SwaggerOperation(OperationId = nameof(Post), Tags = new[] { ApiTags.Cars })]
        [SwaggerRequestExample(typeof(CreateExampleCommandDto), typeof(CreateExampleCommandDtoExample))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateExampleCommandDto request, CancellationToken cancellationToken)
        {
            return await ProcessAsync<CreateExampleCommandDto, CreateExampleCommand>(request, null, cancellationToken);
        }
    }
}
