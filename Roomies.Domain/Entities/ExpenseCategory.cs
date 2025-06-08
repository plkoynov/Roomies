using Roomies.Domain.Interfaces;

namespace Roomies.Domain.Entities;

public class ExpenseCategory : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsDefault { get; set; }

    // Navigation properties
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    public ICollection<ExpenseCategoryRelations> Relations { get; set; } = new List<ExpenseCategoryRelations>();
    public ICollection<HouseholdExpenseCategories> HouseholdExpenseCategories { get; set; } = new List<HouseholdExpenseCategories>();
}
