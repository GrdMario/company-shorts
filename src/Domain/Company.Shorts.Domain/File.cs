namespace Company.Shorts.Domain
{
    using System;

    public class File
    {
        public File(Guid id, string name, byte[] data, string extension, string contentType, DateTimeOffset createdAt)
        {
            Id = id;
            Name = name;
            Data = data;
            Extension = extension;
            ContentType = contentType;
            CreatedAt = createdAt;
        }

        public Guid Id { get; }

        public string Name { get; }

        public byte[] Data { get; }

        public string Extension { get; }

        public string ContentType { get; }

        public DateTimeOffset CreatedAt { get; }
    }
}
