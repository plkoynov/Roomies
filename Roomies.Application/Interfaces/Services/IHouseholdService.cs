using System;
using System.Threading.Tasks;
using Roomies.Application.Models;

namespace Roomies.Application.Interfaces.Services;

public interface IHouseholdService
{
    Task<ServiceResponse<Guid, Enum>> CreateHouseholdAsync(
        CreateHouseholdRequestDto request);
}
