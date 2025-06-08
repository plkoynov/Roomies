using System.ComponentModel;

namespace Roomies.Application.Enums;

public enum ErrorCodes
{
    [Description("Missing user data.")]
    MissingUserData = 1000,

    [Description("Name must not be null, empty, or exceed the maximum length.")]
    InvalidNameFormat = 1001,

    [Description("Email must not be null, empty, exceed the maximum length, and must be in a valid format.")]
    InvalidEmailFormat = 1002,

    [Description("Password must not be null, empty, and must meet complexity requirements.")]
    InvalidPasswordFormat = 1003,

    [Description("User already exists with the provided email.")]
    UserAlreadyExists = 1004,

    [Description("Invalid email or password.")]
    InvalidCredentials = 1005,

    [Description("User not found.")]
    UserNotFound = 1006,

    [Description("Missing household data.")]
    MissingHouseholdData = 2000,

    [Description("Household name must not be null, empty, or exceed the maximum length.")]
    InvalidHouseholdNameFormat = 2001,

    [Description("Household description must not exceed the maximum length.")]
    InvalidDescriptionFormat = 2002,

    [Description("A household with the same name already exists for the user.")]
    HouseholdAlreadyExists = 2003,
}
