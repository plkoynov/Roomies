using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Roomies.Domain.Entities;

namespace Roomies.Persistence.Configurations
{
    public class ExpenseCategoryConfiguration : IEntityTypeConfiguration<ExpenseCategory>
    {
        public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
        {
            // Map Id as the primary key
            builder.HasKey(ec => ec.Id);

            // Set Name with a max length of 100 and required
            builder.Property(ec => ec.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Configure IsDefault as required
            builder.Property(ec => ec.IsDefault)
                .IsRequired();

            // Define a one-to-many relationship with Expense
            builder.HasMany(ec => ec.Expenses)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
