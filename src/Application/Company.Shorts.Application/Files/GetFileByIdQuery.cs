namespace Company.Shorts.Application.Files
{
    using AutoMapper;
    using Company.Shorts.Application.Contracts.Db;
    using Company.Shorts.Application.Files.Models;
    using Company.Shorts.Domain;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public record GetFileByIdQuery(Guid Id) : IRequest<FileResponse>;

    internal sealed class GetFileByIdQueryHandler : IRequestHandler<GetFileByIdQuery, FileResponse>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetFileByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<FileResponse> Handle(GetFileByIdQuery request, CancellationToken cancellationToken)
        {
            File file = await this.unitOfWork.Files.GetbyIdSafeAsync(request.Id, cancellationToken);

            return this.mapper.Map<FileResponse>(file);
        }
    }
}
