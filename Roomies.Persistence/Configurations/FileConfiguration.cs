using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Roomies.Persistence.Configurations;

public class FileConfiguration : IEntityTypeConfiguration<Domain.Entities.File>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.File> builder)
    {
        // Map Id as the primary key
        builder.HasKey(f => f.Id);

        // Set FileName with a max length of 255 and required
        builder.Property(f => f.FileName)
            .HasMaxLength(255)
            .IsRequired();

        // Configure Content as required
        builder.Property(f => f.Content)
            .IsRequired();

        // Set Extension with a max length of 10 and required
        builder.Property(f => f.Extension)
            .HasMaxLength(10)
            .IsRequired();

        // Set MimeType with a max length of 50 and required
        builder.Property(f => f.MimeType)
            .HasMaxLength(50)
            .IsRequired();

        // Configure FileSize as required
        builder.Property(f => f.FileSize)
            .IsRequired();

        // Set Hash with a max length of 64 and required
        builder.Property(f => f.Hash)
            .HasMaxLength(64)
            .IsRequired();

        // Configure CreatedAt as required
        builder.Property(f => f.CreatedAt)
            .IsRequired();

        // Define a one-to-many relationship with ExpenseAttachment using Attachments
        builder.HasMany(f => f.Attachments)
            .WithOne(ea => ea.File)
            .HasForeignKey(ea => ea.FileId)
            .IsRequired();

        // Remove CreatedBy foreign key configuration as it references a Guid
        builder.HasOne(f => f.Creator)
            .WithMany(c => c.Files)
            .HasForeignKey(f => f.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
