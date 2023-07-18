namespace Company.Shorts.Presentation.Api.Controllers.V1
{
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Company.Shorts.Presentation.Api.Internal.Constants;
    using Swashbuckle.AspNetCore.Annotations;
    using Company.Shorts.Application.UserAggregate.Query;
    using Company.Shorts.Domain;
    using Company.Shorts.Presentation.Api.Controllers.V1.Models.Users;
    using Company.Shorts.Application.PetsAggregate.Queries;
    using Company.Shorts.Presentation.Api.Controllers.V1.Models.Pets;

    [ApiVersion(ApiVersions.V1)]
    public class PetsController : ApiControllerBase
    {
        public PetsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        /// <summary>
        /// Gets pets.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(OperationId = nameof(Get), Tags = new[] { ApiTags.Pets })]
        [ProducesResponseType(typeof(List<PetResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] GetPetsDto request)
        {
            return await ProcessAsync<GetPetsDto, GetPetsQuery, List<PetResponse>>(request);
        }
    }
}