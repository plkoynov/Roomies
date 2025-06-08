namespace Roomies.Domain.Interfaces;

public interface IAuditableEntity : IEntity
{
    Guid CreatedBy { get; set; }
    DateTime CreatedAt { get; set; }
}
