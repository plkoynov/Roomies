using System.Threading.Tasks;
using Roomies.Domain.Entities;

namespace Roomies.Application.Interfaces.Repositories
{
    public interface IHouseholdRepository
    {
        Task AddHouseholdAsync(Household household);
        Task<bool> HasSameNameUserHouseholdAsync(string name, Guid userId);
    }
}
