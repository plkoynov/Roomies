using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Roomies.Domain.Entities;

namespace Roomies.Persistence.Configurations
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            // Map Id as the primary key
            builder.HasKey(e => e.Id);

            // Configure HouseholdId as a required foreign key referencing Household
            builder.Property(e => e.HouseholdId)
                .IsRequired();

            builder.HasOne(e => e.Household)
                .WithMany(h => h.Expenses)
                .HasForeignKey(e => e.HouseholdId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure UserId as a required foreign key referencing User
            builder.Property(e => e.UserId)
                .IsRequired();

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure CategoryId as a required foreign key referencing ExpenseCategory
            builder.Property(e => e.CategoryId)
                .IsRequired();

            builder.HasOne(e => e.Category)
                .WithMany(c => c.Expenses)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Set Amount as required with precision (10,2)
            builder.Property(e => e.Amount)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            // Configure Date as required
            builder.Property(e => e.Date)
                .IsRequired();

            // Set Description with a max length of 255 and optional
            builder.Property(e => e.Description)
                .HasMaxLength(255);

            // Configure OwnershipTypeId and PaymentStatusId as required
            builder.Property(e => e.OwnershipTypeId)
                .IsRequired();

            builder.Property(e => e.PaymentStatusId)
                .IsRequired();

            // Configure CreatedAt as required and UpdatedAt as optional
            builder.Property(e => e.CreatedAt)
                .IsRequired();

            // Define one-to-many relationship with ExpenseShare
            builder.HasMany(e => e.Shares)
                .WithOne(s => s.Expense)
                .HasForeignKey(s => s.ExpenseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
