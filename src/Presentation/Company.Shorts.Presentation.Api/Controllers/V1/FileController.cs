namespace Company.Shorts.Presentation.Api.Controllers.V1
{
    using Company.Shorts.Application.Files;
    using Company.Shorts.Application.Files.Models;
    using Company.Shorts.Presentation.Api.Internal.Constants;
    using Company.Shorts.Presentation.Api.Internal.Extensions;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;

    [ApiVersion(ApiVersions.V1)]
    public class FileController : ApiControllerBase
    {
        public FileController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [SwaggerOperation(OperationId = nameof(Get), Tags = new[] { ApiTags.Files })]
        [ProducesResponseType(typeof(List<FileResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] GetFilesQuery query, CancellationToken cancellationToken)
        {
            return await this.ProcessAsync<GetFilesQuery, List<FileResponse>>(query, cancellationToken);
        }

        [HttpPost]
        [SwaggerOperation(OperationId = nameof(Post), Tags = new[] { ApiTags.Files })]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(IFormFile formFile, CancellationToken cancellationToken)
        {
            var command = await formFile.AsCreateFileCommandAsync(cancellationToken);

            return await this.ProcessAsync<CreateFileCommand, FileResponse>(command, cancellationToken);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = nameof(GetById), Tags = new[] { ApiTags.Files })]
        [ProducesResponseType(typeof(FileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            return await this.ProcessAsync<GetFileByIdQuery, FileResponse>(new GetFileByIdQuery(id), cancellationToken);
        }

        [HttpGet("{id}/download")]
        [SwaggerOperation(OperationId = nameof(DownloadById), Tags = new[] { ApiTags.Files })]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<FileContentResult> DownloadById(Guid id, CancellationToken cancellationToken)
        {
            FileDownloadResponse result = await this.Mediator.Send(new DownloadFileByIdQuery(id), cancellationToken);

            return new FileContentResult(result.Data, result.ContentType)
            {
                FileDownloadName = result.Name
            };
        }
    }
}