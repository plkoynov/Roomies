using Roomies.Domain.Enums;
using Roomies.Domain.Interfaces;

namespace Roomies.Domain.Entities;

public class ExpenseShare : IAuditableEntity
{
    public Guid Id { get; set; }
    public Guid ExpenseId { get; set; }
    public Guid UserId { get; set; }
    public decimal ShareAmount { get; set; }
    public PaymentStatus PaymentStatusId { get; set; }
    public Guid? StatusUpdatedBy { get; set; }
    public DateTime? StatusUpdatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public Expense Expense { get; set; }
    public User UpdatedBy { get; set; }
    public User Creator { get; set; }
    public User User { get; set; }
}
