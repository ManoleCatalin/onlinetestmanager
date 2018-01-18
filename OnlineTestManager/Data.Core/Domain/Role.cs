using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Data.Core.Domain
{
    public class Role : IdentityRole<Guid>, IBaseEntity
    {
        public ICollection<UserRole> UserRoles { get; set; }
        public bool IsDeleted { get; set; }
        public Role()
        {
            //Ef core needs this
        }

        protected Role(string name) : base(name)
        {
        }

        public static Role Create(string name)
        {
            var instance = new Role(name);
            return instance;
        }

        public void Update(string name)
        { 
            Name = name;
        }
    }
}