using Microsoft.EntityFrameworkCore;
using Roomies.Application.Interfaces.Repositories;
using Roomies.Domain.Entities;

namespace Roomies.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RoomiesDbContext _context;

        public UserRepository(RoomiesDbContext context)
        {
            _context = context;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
    }
}
