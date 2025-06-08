using Roomies.Domain.Interfaces;

namespace Roomies.Domain.Entities;

public class ExpenseAttachment : IEntity
{
    public Guid Id { get; set; }
    public Guid ExpenseId { get; set; }
    public Guid FileId { get; set; }
    public string? Name { get; set; }

    // Navigation properties
    public Expense Expense { get; set; }
    public File File { get; set; }
}
