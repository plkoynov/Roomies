using FluentValidation;
using Roomies.Application.Constants;
using Roomies.Presentation.Models;

namespace Roomies.Presentation.Validators;

public class CreateHouseholdRequestValidator : AbstractValidator<CreateHouseholdRequest>
{
    public CreateHouseholdRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(CommonConstants.MaxNameLength).WithMessage($"Name must not exceed {CommonConstants.MaxNameLength} characters.");

        RuleFor(x => x.Description)
            .MaximumLength(CommonConstants.MaxDescriptionLength).WithMessage($"Description must not exceed {CommonConstants.MaxDescriptionLength} characters.");
    }
}
