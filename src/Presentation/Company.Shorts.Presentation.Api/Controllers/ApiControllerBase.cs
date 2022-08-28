namespace Company.Shorts.Presentation.Api.Controllers
{
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Net.Mime;

    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        protected ApiControllerBase(
            IMediator mediator,
            IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        protected async Task<IActionResult> ProcessAsync<TApiDto, TCommand, TResponse, TResponseDto>(
            TApiDto request,
            Action<IMappingOperationOptions<object, TCommand>>? opts = null)
            where TApiDto : IApiDto
            where TCommand : IRequest<TResponse>
        {
            TCommand? command = opts is not null
                ? this.mapper.Map<TCommand>(request, opts)
                : this.mapper.Map<TCommand>(request);

            TResponse result = await this.mediator.Send(command);

            if (result is null)
            {
                return this.NotFound();
            }

            var mapped = mapper.Map<TResponseDto>(result);

            return this.Ok(mapped);
        }

        protected async Task<IActionResult> ProcessAsync<TApiDto, TCommand>(
            TApiDto request,
            Action<IMappingOperationOptions<object, TCommand>>? opts = null)
            where TApiDto : IApiDto
            where TCommand : IRequest
        {
            TCommand command = opts is not null
                ? this.mapper.Map<TCommand>(request, opts)
                : this.mapper.Map<TCommand>(request);

            await this.mediator.Send(command);

            return this.NoContent();
        }
    }
}
