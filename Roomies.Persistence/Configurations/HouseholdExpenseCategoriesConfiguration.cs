using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Roomies.Domain.Entities;

namespace Roomies.Persistence.Configurations
{
    public class HouseholdExpenseCategoriesConfiguration : IEntityTypeConfiguration<HouseholdExpenseCategories>
    {
        public void Configure(EntityTypeBuilder<HouseholdExpenseCategories> builder)
        {
            // Configure composite primary key
            builder.HasKey(hec => new { hec.HouseholdId, hec.ExpenseCategoryId });

            // Configure foreign key for HouseholdId
            builder.HasOne(hec => hec.Household)
                .WithMany(h => h.HouseholdExpenseCategories)
                .HasForeignKey(hec => hec.HouseholdId)
                .IsRequired();

            // Configure foreign key for ExpenseCategoryId
            builder.HasOne(hec => hec.ExpenseCategory)
                .WithMany(ec => ec.HouseholdExpenseCategories)
                .HasForeignKey(hec => hec.ExpenseCategoryId)
                .IsRequired();
        }
    }
}
