using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roomies.Application.Interfaces.Services;
using Roomies.Presentation.Models;

namespace Roomies.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class HouseholdController : ControllerBase
{
    private readonly IHouseholdService _householdService;
    private readonly IValidator<CreateHouseholdRequest> _createValidator;

    public HouseholdController(
        IHouseholdService householdService,
        IValidator<CreateHouseholdRequest> createValidator)
    {
        _householdService = householdService;
        _createValidator = createValidator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateHousehold([FromBody] CreateHouseholdRequest request)
    {
        var validationResult = await _createValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var result = await _householdService.CreateHouseholdAsync(
            request.ToApplicationModel());

        if (!result.IsSuccess)
        {
            return StatusCode(result.StatusCode, result.Error);
        }

        return Ok(result.Data);
    }
}