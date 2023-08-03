namespace Company.Shorts.Application.Files
{
    using AutoMapper;
    using Company.Shorts.Application.Contracts.Db;
    using Company.Shorts.Application.Files.Models;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public record DownloadFileByIdQuery(Guid Id) : IRequest<FileDownloadResponse>;

    internal sealed class DownloadFileByIdQueryHandler : IRequestHandler<DownloadFileByIdQuery, FileDownloadResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DownloadFileByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<FileDownloadResponse> Handle(DownloadFileByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await this.unitOfWork.Files.GetbyIdSafeAsync(request.Id, cancellationToken);

            return this.mapper.Map<FileDownloadResponse>(result);
        }
    }
}
