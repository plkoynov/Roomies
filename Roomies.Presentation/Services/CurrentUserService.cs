using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Roomies.Application.Interfaces.Services;
using Roomies.Application.Models;

namespace Roomies.Presentation.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user == null)
                {
                    throw new InvalidOperationException("No user is currently authenticated.");
                }

                var id = user.FindFirstValue(ClaimTypes.NameIdentifier);
                return Guid.Parse(id);
            }
        }

        public CurrentUserDto GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null)
            {
                throw new InvalidOperationException("No user is currently authenticated.");
            }

            var id = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
            var name = user.FindFirstValue(ClaimTypes.Name);
            var email = user.FindFirstValue(ClaimTypes.Email);

            return new CurrentUserDto
            {
                Id = id,
                Name = name,
                Email = email
            };
        }
    }
}
