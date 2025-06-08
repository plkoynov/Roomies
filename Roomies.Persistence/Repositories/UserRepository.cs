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
        => await _context.Users
            .AnyAsync(u => u.Email.Trim().ToLower() == email.Trim().ToLower());

        public async Task AddUserAsync(User user)
        => await _context.Users.AddAsync(user);

        public async Task<User?> GetUserByEmailAsync(string email)
        => await _context.Users
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.Trim().ToLower());
    }
}
