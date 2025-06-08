using Roomies.Application.Models;
using Roomies.Presentation.Models;

public static class AuthMappings
{
    public static RegisterUserDto ToRegisterUserDto(
        this RegisterUserRequest request,
        string tokenSecret)
    {
        return new RegisterUserDto
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            TokenSecret = tokenSecret
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
}