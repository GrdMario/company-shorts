namespace Company.Shorts.Presentation.Api.Controllers.V1
{
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Company.Shorts.Presentation.Api.Internal.Constants;
    using Microsoft.AspNetCore.Http;
    using Company.Shorts.Presentation.Api.Internal.Services;
    using Microsoft.AspNetCore.JsonPatch.Operations;
    using Swashbuckle.AspNetCore.Annotations;

    [ApiVersion(ApiVersions.V1)]
    public class FileController : ApiControllerBase
    {
        public FileController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]

        [SwaggerOperation(OperationId = nameof(Post), Tags = new[] { ApiTags.Files })]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(IFormFile formFile)
        {
            var command = await formFile.AsCreateFileCommandAsync();

            var result = await this.Mediator.Send(command);

            return Ok(result);
        }
    }
}