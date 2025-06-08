using Roomies.Domain.Interfaces;

namespace Roomies.Domain.Entities;

public class Household : IAuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public ICollection<HouseholdMember> Members { get; set; } = new List<HouseholdMember>();
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    public ICollection<HouseholdExpenseCategories> HouseholdExpenseCategories { get; set; } = new List<HouseholdExpenseCategories>();
}
