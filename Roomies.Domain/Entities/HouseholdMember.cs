using Roomies.Domain.Enums;
using Roomies.Domain.Interfaces;

namespace Roomies.Domain.Entities;

public class HouseholdMember : IEntity
{
    public Guid Id { get; set; }
    public Guid HouseholdId { get; set; }
    public Guid UserId { get; set; }
    public MemberType MemberTypeId { get; set; }
    public DateTime JoinedAt { get; set; }

    // Navigation properties
    public Household Household { get; set; }
    public User User { get; set; }
}
