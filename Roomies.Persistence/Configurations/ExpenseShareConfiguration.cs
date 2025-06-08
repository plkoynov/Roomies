using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Roomies.Domain.Entities;

namespace Roomies.Persistence.Configurations
{
    public class ExpenseShareConfiguration : IEntityTypeConfiguration<ExpenseShare>
    {
        public void Configure(EntityTypeBuilder<ExpenseShare> builder)
        {
            // Map Id as the primary key
            builder.HasKey(es => es.Id);

            // Configure ExpenseId as a required foreign key referencing Expense
            builder.Property(es => es.ExpenseId)
                .IsRequired();

            builder.HasOne(es => es.Expense)
                .WithMany(e => e.Shares)
                .HasForeignKey(es => es.ExpenseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure UserId as a required foreign key referencing User
            builder.Property(es => es.UserId)
                .IsRequired();

            builder.HasOne(es => es.User)
                .WithMany()
                .HasForeignKey(es => es.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Set ShareAmount as required with precision (10,2)
            builder.Property(es => es.ShareAmount)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            // Configure PaymentStatusId as required
            builder.Property(es => es.PaymentStatusId)
                .IsRequired();

            // Configure StatusUpdatedBy as an optional foreign key referencing User
            builder.HasOne(es => es.UpdatedBy)
                .WithMany()
                .HasForeignKey(es => es.StatusUpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Adjusted to use `Creator` and `UpdatedBy` navigation properties instead of `User`.
            builder.HasOne(es => es.Creator)
                .WithMany()
                .HasForeignKey(es => es.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
