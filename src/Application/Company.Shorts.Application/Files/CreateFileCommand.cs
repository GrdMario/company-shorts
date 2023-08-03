namespace Company.Shorts.Application.Files
{
    using AutoMapper;
    using Company.Shorts.Application.Contracts.Security;
    using Company.Shorts.Application.Files.Models;
    using Company.Shorts.Blocks.Application.Contracts;
    using Company.Shorts.Domain;
    using Company.Shorts.Domain.Seedwork;
    using FluentValidation;
    using MediatR;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using IUnitOfWork = Contracts.Db.IUnitOfWork;

    public class CreateFileCommand : IRequest<FileResponse>
    {
        public CreateFileCommand(string name, byte[] data, string extension, string contentDisposition, string contentType)
        {
            Name = name;
            Data = data;
            Extension = extension;
            ContentDisposition = contentDisposition;
            ContentType = contentType;
        }

        public string Name { get; }

        public byte[] Data { get; }

        public string Extension { get; }

        public string ContentDisposition { get; }

        public string ContentType { get; }
    }

    internal sealed class CreateFileCommandValidator : AbstractValidator<CreateFileCommand>
    {
        private const int AllowedFileSize = 1048576 * 3;
        private const string FormData = "form-data";

        private readonly IAntivirusService antivirusService;

        public CreateFileCommandValidator(IAntivirusService antivirusService)
        {
            this.antivirusService = antivirusService;

            this.RuleFor(r => r.Name)
                .NotEmpty();

            this.RuleFor(r => r.ContentDisposition)
                .NotEmpty()
                .Must(IsContentDispositionFormData)
                .WithMessage(command => $"Content-Disposition: '{command.ContentDisposition}' is invalid.");

            this.RuleFor(r => r.Extension)
                .NotEmpty()
                .Must((command, _) => IsValidFileExtension(command))
                .WithMessage(command => $"Extension: '{command.Extension}' is not supported.");

            this.RuleFor(r => r.Data)
                .NotEmpty()
                .Must(IsValidFileLenght)
                .WithMessage("File is to large for upload.")
                .MustAsync(async (s, a) => !(await IsVirus(s)))
                .WithMessage("Potentally harmful file detected.");
        }

        /// <summary>
        /// Checks if content disposition contains form-data.
        /// </summary>
        /// <param name="contentDisposition">Content disposition</param>
        /// <returns>True if correct content disposition is provided.</returns>
        private bool IsContentDispositionFormData(string contentDisposition)
        {
            if (string.IsNullOrWhiteSpace(contentDisposition))
            {
                return false;
            }

            return contentDisposition.Contains(FormData);
        }

        /// <summary>
        /// Checks if file is a virus.
        /// </summary>
        /// <param name="data">File</param>
        /// <returns>True if file is a virus.</returns>
        private async Task<bool> IsVirus(byte[] data) => await this.antivirusService.IsVirusAsync(data);

        /// <summary>
        /// Checks file size
        /// </summary>
        /// <param name="data">File data.</param>
        /// <returns>True if file is not larger than 3 MB</returns>
        private static bool IsValidFileLenght(byte[] data) => data.Length > 0 && data.Length < AllowedFileSize;

        /// <summary>
        /// Checks files extension and file signature.
        /// </summary>
        /// <param name="command">Command for file upload.</param>
        /// <returns>True when file extension is allowed and file signature is correct.</returns>
        private static bool IsValidFileExtension(CreateFileCommand command)
        {
            if (string.IsNullOrEmpty(command.Extension))
            {
                return false;
            }

            var currentExtension = Enumeration.FromDisplayName<FileExtension>(command.Extension);

            if (currentExtension is null)
            {
                return false;
            }

            using var stream = new MemoryStream(command.Data);
            using var reader = new BinaryReader(stream);
            var signatures = currentExtension.Signatures;

            if (signatures.Count == 0)
            {
                return true;
            }

            var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

            return signatures.Any(signature =>
                headerBytes.Take(signature.Length).SequenceEqual(signature));
        }
    }

    internal sealed class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, FileResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateFileCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<FileResponse> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            var file = new File(SystemGuid.NewGuid, request.Name, request.Data, request.Extension, request.ContentType, SystemClock.UtcNow);

            this.unitOfWork.Files.Add(file);

            await this.unitOfWork.SaveAsync(cancellationToken);

            return this.mapper.Map<FileResponse>(file);
        }
    }
}
