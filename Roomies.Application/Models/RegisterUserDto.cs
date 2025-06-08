namespace Roomies.Application.Models;

public class RegisterUserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string TokenSecret { get; set; }
}
