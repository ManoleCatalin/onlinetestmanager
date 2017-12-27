using System;
using Microsoft.AspNetCore.Identity;

namespace Data.Core.Domain
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public User User { get; set; }
        public Role Role { get; set; }

        public UserRole()
        {
            //Ef core needs this
        }
    }
}
