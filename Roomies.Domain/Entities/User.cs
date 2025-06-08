using Roomies.Domain.Interfaces;

namespace Roomies.Domain.Entities;

public class User : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public ICollection<Household> Households { get; set; } = new List<Household>();
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    public ICollection<File> Files { get; set; } = new List<File>();
}
