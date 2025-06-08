using Roomies.Application.Models;

namespace Roomies.Application.Interfaces.Services
{
    public interface ICurrentUserService
    {
        CurrentUserDto GetCurrentUser();

        Guid UserId { get; }
    }
}
