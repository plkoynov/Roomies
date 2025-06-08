using Roomies.Application.Models;
using Roomies.Presentation.Models;

public static class HouseholdMappings
{
    public static CreateHouseholdRequestDto ToApplicationModel(
        this CreateHouseholdRequest request)
    {
        return new CreateHouseholdRequestDto
        {
            Name = request.Name.Trim(),
            Description = request.Description?.Trim()
        };
    }
}