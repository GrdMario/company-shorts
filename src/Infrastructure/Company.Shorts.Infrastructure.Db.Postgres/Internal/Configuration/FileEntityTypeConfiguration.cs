namespace Company.Shorts.Infrastructure.Db.Postgres.Internal.Configuration
{
    using Company.Shorts.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal sealed class FileEntityTypeConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.ToTable("files");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Data).IsRequired();

            builder.Property(p => p.Extension).IsRequired();

            builder.Property(p => p.CreatedAt).IsRequired();
        }
    }
}
