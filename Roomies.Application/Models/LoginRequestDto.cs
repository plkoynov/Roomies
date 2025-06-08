namespace Roomies.Application.Models
{
    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string TokenSecret { get; set; }
    }
}
