using Roomies.Domain.Enums;
using Roomies.Domain.Interfaces;

namespace Roomies.Domain.Entities;

public class Expense : IAuditableEntity
{
    public Guid Id { get; set; }
    public Guid HouseholdId { get; set; }
    public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public OwnershipType OwnershipTypeId { get; set; }
    public PaymentStatus PaymentStatusId { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public Household Household { get; set; }
    public User User { get; set; }
    public ExpenseCategory Category { get; set; }
    public ICollection<ExpenseShare> Shares { get; set; } = new List<ExpenseShare>();
    public ICollection<ExpenseAttachment> Attachments { get; set; } = new List<ExpenseAttachment>();
}
