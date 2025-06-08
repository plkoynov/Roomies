using Roomies.Domain.Interfaces;

namespace Roomies.Domain.Entities
{
    public class HouseholdExpenseCategories : IEntity
    {
        public Guid Id { get; set; }
        public Guid HouseholdId { get; set; }
        public Guid ExpenseCategoryId { get; set; }

        // Navigation properties
        public Household Household { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
    }
}
