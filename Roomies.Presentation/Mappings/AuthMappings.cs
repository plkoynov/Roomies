using Roomies.Application.Models;
using Roomies.Presentation.Models;

public static class AuthMappings
{
    public static RegisterUserDto ToRegisterUserDto(
        this RegisterUserRequest request)
    {
        return new RegisterUserDto
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password
        };
    }

    public static RegisterUserResponse ToRegisterUserResponse(
        this RegisterUserResult registerUserResult)
    {
        return new RegisterUserResponse
        {
            Token = registerUserResult.Token
        };
    }

    public static LoginRequestDto ToLoginRequestDto(
        this LoginRequest request)
    {
        return new LoginRequestDto
        {
            Email = request.Email,
            Password = request.Password
        };
    }
}