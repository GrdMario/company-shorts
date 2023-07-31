namespace Company.Shorts.Domain
{
    using System;

    public class File
    {
        public File(Guid id, byte[] data, string extension, DateTimeOffset createdAt)
        {
            Id = id;
            Data = data;
            Extension = extension;
            this.CreatedAt = createdAt;
        }

        public Guid Id { get; }

        public byte[] Data { get; }

        public string Extension { get; }

        public DateTimeOffset CreatedAt { get; }
    }
}
