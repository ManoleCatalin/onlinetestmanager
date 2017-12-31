using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace OTM.UserContext
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? GetLogedInUserId()
        {
            var logedInUser = _httpContextAccessor.HttpContext.User;
            if (logedInUser == null)
                return null;

            return new Guid(logedInUser.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
