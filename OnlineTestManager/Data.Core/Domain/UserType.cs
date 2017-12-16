using System;
using Microsoft.AspNetCore.Identity;

namespace Data.Core.Domain
{
    public class UserType : IdentityRole<Guid>, IBaseEntity
    {
        public UserType()
        {
            //Ef core needs this
        }

        protected UserType(string name) : base(name)
        {
        }

        public static UserType Create(string name)
        {
            var instance = new UserType(name);
            return instance;
        }

        public void Update(string name)
        { 
            Name = name;
        }
    }
}