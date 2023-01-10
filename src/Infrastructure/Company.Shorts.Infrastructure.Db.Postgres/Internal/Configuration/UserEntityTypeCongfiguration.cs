namespace Company.Shorts.Infrastructure.Db.Postgres.Internal.Configuration
{
    using Company.Shorts.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal sealed class UserEntityTypeCongfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("users");

            builder
                .HasKey(key => key.Id);
            
            builder
                .Property(p => p.UserName)
                .IsRequired()
                .HasMaxLength(100);
            
            builder
                .Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(p => p.Address)
                .IsRequired();

            builder
                .Property(p => p.ProfilePicture)
                .IsRequired();
        }
    }
}
