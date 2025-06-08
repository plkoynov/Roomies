using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Roomies.Domain.Entities;

namespace Roomies.Persistence.Configurations;

public class ExpenseAttachmentConfiguration : IEntityTypeConfiguration<ExpenseAttachment>
{
    public void Configure(EntityTypeBuilder<ExpenseAttachment> builder)
    {
        // Map Id as the primary key
        builder.HasKey(ea => ea.Id);

        // Configure ExpenseId as a required foreign key referencing Expense
        builder.HasOne(ea => ea.Expense)
            .WithMany(e => e.Attachments)
            .HasForeignKey(ea => ea.ExpenseId)
            .IsRequired();

        // Configure FileId as a required foreign key referencing File
        builder.HasOne(ea => ea.File)
            .WithMany(f => f.Attachments)
            .HasForeignKey(ea => ea.FileId)
            .IsRequired();

        // Set Name with a max length of 255 and optional
        builder.Property(ea => ea.Name)
            .HasMaxLength(255)
            .IsRequired(false);
    }
}
