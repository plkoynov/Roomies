using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Roomies.Domain.Entities;

namespace Roomies.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Map Id as the primary key
            builder.HasKey(u => u.Id);

            // Set Name with a max length of 100 and required
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Set Email with a max length of 255 and required
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            // Set Password with a max length of 255 and required
            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255);

            // Configure CreatedAt as required
            builder.Property(u => u.CreatedAt)
                .IsRequired();

            // Define one-to-many relationship with Household
            builder.HasMany(u => u.Households)
                .WithOne()
                .HasForeignKey(h => h.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Define one-to-many relationship with Expense
            builder.HasMany(u => u.Expenses)
                .WithOne()
                .HasForeignKey(e => e.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
