using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Roomies.Domain.Entities;

namespace Roomies.Persistence.Configurations
{
    public class HouseholdMemberConfiguration : IEntityTypeConfiguration<HouseholdMember>
    {
        public void Configure(EntityTypeBuilder<HouseholdMember> builder)
        {
            // Map Id as the primary key
            builder.HasKey(hm => hm.Id);

            // Configure HouseholdId as a required foreign key referencing Household
            builder.Property(hm => hm.HouseholdId)
                .IsRequired();

            builder.HasOne(hm => hm.Household)
                .WithMany(h => h.Members)
                .HasForeignKey(hm => hm.HouseholdId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure UserId as a required foreign key referencing User
            builder.Property(hm => hm.UserId)
                .IsRequired();

            builder.HasOne(hm => hm.User)
                .WithMany()
                .HasForeignKey(hm => hm.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure MemberType as required
            builder.Property(hm => hm.MemberTypeId)
                .IsRequired();

            // Configure JoinedAt as required
            builder.Property(hm => hm.JoinedAt)
                .IsRequired();
        }
    }
}
