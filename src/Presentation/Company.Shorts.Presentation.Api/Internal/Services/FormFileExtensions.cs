namespace Company.Shorts.Presentation.Api.Internal.Services
{
    using Company.Shorts.Application.Files;
    using Company.Shorts.Domain;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal static class FormFileExtensions
    {
        public static async Task<CreateFileCommand> AsCreateFileCommandAsync(this IFormFile file)
        {
            if (file is null)
            {
                throw new ApplicationException("Unable to resolve file");
            }

            using var stream = new MemoryStream();

            await file.CopyToAsync(stream);

            var bytes = stream.ToArray();

            var result = new CreateFileCommand(bytes, file.ContentType, file.ContentDisposition);

            return result;
        }
    }
}
