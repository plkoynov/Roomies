using Roomies.Domain.Entities;

namespace Roomies.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<bool> EmailExistsAsync(string email);
    Task AddUserAsync(User user);
    Task<User> GetUserByEmailAsync(string email);
}
