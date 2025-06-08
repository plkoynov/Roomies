using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Roomies.Domain.Entities;

namespace Roomies.Persistence.Configurations
{
    public class HouseholdConfiguration : IEntityTypeConfiguration<Household>
    {
        public void Configure(EntityTypeBuilder<Household> builder)
        {
            // Map Id as the primary key
            builder.HasKey(h => h.Id);

            // Set Name with a max length of 100 and required
            builder.Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(h => h.Description)
                .HasMaxLength(500);

            // Configure CreatedBy as a required foreign key referencing User
            builder.Property(h => h.CreatedBy)
                .IsRequired();

            builder.HasOne<User>()
                .WithMany(u => u.Households)
                .HasForeignKey(h => h.CreatedBy)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure CreatedAt as required
            builder.Property(h => h.CreatedAt)
                .IsRequired();

            // Define one-to-many relationship with HouseholdMember
            builder.HasMany(h => h.Members)
                .WithOne(m => m.Household)
                .HasForeignKey(m => m.HouseholdId)
                .OnDelete(DeleteBehavior.Cascade);

            // Define one-to-many relationship with Expense
            builder.HasMany(h => h.Expenses)
                .WithOne(e => e.Household)
                .HasForeignKey(e => e.HouseholdId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
