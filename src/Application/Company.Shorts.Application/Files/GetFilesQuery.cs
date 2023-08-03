namespace Company.Shorts.Application.Files
{
    using AutoMapper;
    using Company.Shorts.Application.Contracts.Db;
    using Company.Shorts.Application.Files.Models;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public record GetFilesQuery(int Skip = 0, int Take = 20) : IRequest<List<FileResponse>>;

    internal sealed class GetFilesQueryHandler : IRequestHandler<GetFilesQuery, List<FileResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetFilesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<FileResponse>> Handle(GetFilesQuery request, CancellationToken cancellationToken)
        {
            var files = await this.unitOfWork.Files.GetAsync(request.Skip, request.Take, cancellationToken);

            return this.mapper.Map<List<FileResponse>>(files);
        }
    }
}
