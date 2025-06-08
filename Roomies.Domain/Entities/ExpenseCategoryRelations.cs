using Roomies.Domain.Interfaces;

namespace Roomies.Domain.Entities
{
    public class ExpenseCategoryRelations : IEntity
    {
        public Guid Id { get; set; }
        public Guid AncestorId { get; set; }
        public Guid DescendantId { get; set; }
        public int Depth { get; set; }

        // Navigation properties
        public ExpenseCategory Ancestor { get; set; }
        public ExpenseCategory Descendant { get; set; }
    }
}
