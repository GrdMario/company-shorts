namespace Company.Shorts.Presentation.Api.Controllers.V1
{
    using AutoMapper;
    using Company.Shorts.Application.ExampleAggregate.Command;
    using Company.Shorts.Application.ExampleAggregate.Common.Responses;
    using Company.Shorts.Application.ExampleAggregate.Query;
    using Company.Shorts.Presentation.Api.Controllers.V1.Models.Examples;
    using Company.Shorts.Presentation.Api.Internal.Constants;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;

    [ApiVersion(ApiVersions.V1)]
    public class ExamplesController : ApiControllerBase
    {
        public ExamplesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        /// <summary>
        /// Gets examples.
        /// </summary>
        /// <param name="query">Model for quering examples.</param>
        [HttpGet]
        [SwaggerOperation(OperationId = nameof(Get), Tags = new[] { ApiTags.Examples })]
        [ProducesResponseType(typeof(List<ExampleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] GetExamplesQueryDto request)
        {
            return await ProcessAsync<GetExamplesQueryDto, GetExamplesQuery, List<ExampleResponse>>(request);
        }

        /// <summary>
        /// Creates example.
        /// </summary>
        [HttpPost]
        [SwaggerOperation(OperationId = nameof(Post), Tags = new[] { ApiTags.Examples })]
        [ProducesResponseType(typeof(ExampleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateExampleCommandDto request)
        {
            return await ProcessAsync<CreateExampleCommandDto, CreateExampleCommand>(request);
        }

        /// <summary>
        /// Updates example.
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerOperation(OperationId = nameof(Put), Tags = new[] { ApiTags.Examples })]
        [ProducesResponseType(typeof(ExampleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateExampleCommandDto request)
        {
            return await ProcessAsync<UpdateExampleCommandDto, UpdateExampleCommand>(request, opt => opt.AfterMap((_, command) =>
            {
                command.Id = id;
            }));
        }

        /// <summary>
        /// Gets example by id.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = nameof(GetById), Tags = new[] { ApiTags.Examples })]
        [ProducesResponseType(typeof(ExampleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            return await ProcessAsync<GetExampleQueryDto, GetExampleQuery, ExampleResponse>(new GetExampleQueryDto(id));
        }
    }
}
