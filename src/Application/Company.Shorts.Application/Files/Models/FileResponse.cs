namespace Company.Shorts.Application.Files.Models
{
    using System;
    using Company.Shorts.Domain;

    public class FileResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public string Extension { get; set; } = default!;

        public DateTimeOffset CreatedAt { get; set; }

        public string ContentType { get; set; } = default!;

        public string DownloadUrl { get; set; } = default!;
    }
}
