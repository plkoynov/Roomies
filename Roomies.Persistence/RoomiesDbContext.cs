using Microsoft.EntityFrameworkCore;
using Roomies.Domain.Entities;

namespace Roomies.Persistence
{
    public class RoomiesDbContext : DbContext
    {
        public RoomiesDbContext(DbContextOptions<RoomiesDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Household> Households { get; set; }
        public DbSet<HouseholdMember> HouseholdMembers { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseShare> ExpenseShares { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<ExpenseCategoryRelations> ExpenseCategoryRelations { get; set; }
        public DbSet<HouseholdExpenseCategories> HouseholdExpenseCategories { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<ExpenseAttachment> ExpenseAttachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoomiesDbContext).Assembly);
        }
    }
}
