using System;
using System.Collections.Generic;

namespace Data.Core.Domain
{
    public class User
    {
        public  Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public Guid UserTypeId { get; private set; }
        public virtual UserType UserType { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }
        public ICollection<Grade> Grades { get; set; }


        public static User Create(string firstName, string lastName, string email, string password, Guid userTypeId)
        {
            var instance = new User { Id = Guid.NewGuid() };
            instance.Update(firstName, lastName, email, password, userTypeId);
            return instance;
        }

        public void Update(string firstName, string lastName, string email, string passwordHash, Guid userTypeId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            UserTypeId = userTypeId;
        }
    }
}
