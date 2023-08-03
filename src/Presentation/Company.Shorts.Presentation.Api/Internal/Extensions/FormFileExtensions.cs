namespace Company.Shorts.Presentation.Api.Internal.Extensions
{
    using Company.Shorts.Application.Files;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Threading.Tasks;

    internal static class FormFileExtensions
    {
        public static async Task<CreateFileCommand> AsCreateFileCommandAsync(this IFormFile file, CancellationToken cancellationToken)
        {
            if (file is null)
            {
                throw new ApplicationException("Unable to resolve file");
            }

            string untrustedFileName = Path.GetFileNameWithoutExtension(file.FileName).ToLowerInvariant();

            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            using var stream = new MemoryStream();

            await file.CopyToAsync(stream, cancellationToken);

            var bytes = stream.ToArray();

            var result = new CreateFileCommand(untrustedFileName, bytes, extension, file.ContentDisposition, file.ContentType);

            return result;
        }
    }
}
