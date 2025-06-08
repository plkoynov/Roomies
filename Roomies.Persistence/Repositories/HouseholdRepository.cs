using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Roomies.Application.Interfaces.Repositories;
using Roomies.Domain.Entities;

namespace Roomies.Persistence.Repositories
{
    public class HouseholdRepository : IHouseholdRepository
    {
        private readonly RoomiesDbContext _context;

        public HouseholdRepository(RoomiesDbContext context)
        {
            _context = context;
        }

        public async Task AddHouseholdAsync(Household household)
        {
            await _context.Households.AddAsync(household);
        }

        public async Task<bool> HasSameNameUserHouseholdAsync(string name, Guid userId)
        {
            return await _context.Households
                .AnyAsync(h => h.Name.ToLower() == name.Trim().ToLower() && h.CreatedBy == userId);
        }
    }
}
