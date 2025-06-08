using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Roomies.Domain.Entities;

namespace Roomies.Persistence.Configurations
{
    public class ExpenseCategoryRelationsConfiguration : IEntityTypeConfiguration<ExpenseCategoryRelations>
    {
        public void Configure(EntityTypeBuilder<ExpenseCategoryRelations> builder)
        {
            // Configure AncestorId and DescendantId as a composite primary key
            builder.HasKey(ecr => new { ecr.AncestorId, ecr.DescendantId });

            // Configure AncestorId as a required foreign key referencing ExpenseCategory
            builder.HasOne(ecr => ecr.Ancestor)
                .WithMany(ec => ec.Relations)
                .HasForeignKey(ecr => ecr.AncestorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure DescendantId as a required foreign key referencing ExpenseCategory
            builder.HasOne(ecr => ecr.Descendant)
                .WithMany()
                .HasForeignKey(ecr => ecr.DescendantId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Depth as required
            builder.Property(ecr => ecr.Depth)
                .IsRequired();
        }
    }
}
