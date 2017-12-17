using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Data.Core.Domain
{
    public class User : IdentityUser<Guid>, IBaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Guid UserTypeId { get; private set; }
        public virtual UserType UserType { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }
        public ICollection<Grade> Grades { get; set; }

        public User()
        {
            //Ef core needs this
        }

        public static User Create(string firstName, string lastName, string username, string email, string password, Guid userTypeId)
        {
            var instance = new User { Id = Guid.NewGuid() };
            instance.Update(firstName, lastName, username, email, password, userTypeId);
            return instance;
        }

        public void Update(string firstName, string lastName, string username, string email, string passwordHash, Guid userTypeId)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = username;
            Email = email;
            PasswordHash = passwordHash;
            UserTypeId = userTypeId;
        }
    }
}
