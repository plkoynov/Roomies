using Roomies.Application.Interfaces;

namespace Roomies.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RoomiesDbContext _context;

        public UnitOfWork(RoomiesDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
