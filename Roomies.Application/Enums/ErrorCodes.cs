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
}
