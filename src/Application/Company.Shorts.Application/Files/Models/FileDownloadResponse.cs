namespace Company.Shorts.Application.Files.Models
{
    using System;

    public class FileDownloadResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public byte[] Data { get; set; } = default!;

        public string Extension { get; set; } = default!;

        public DateTimeOffset CreatedAt { get; set; }

        public string ContentType { get; set; } = default!;

        public string ContentDisposition { get; set; } = default!;
    }
}
