using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;

namespace Data.Core.Domain
{
    public class User : IdentityUser<Guid>, IBaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public UserRole UserRole { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }
        public ICollection<Grade> Grades { get; set; }
		public ICollection<ExerciseResponse> ExerciseResponses { get; set; }		public bool IsDeleted { get; set; }        public static User Create(string firstName, string lastName, string username, string email, string password)
        {
            var instance = new User { Id = Guid.NewGuid() };
           
            instance.Update(firstName, lastName, username, email, password,false);
            return instance;
        }

        public void Update(string firstName, string lastName, string username, string email, string passwordHash, bool isDeleted)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = username;
            Email = email;
            PasswordHash = passwordHash;
            IsDeleted = isDeleted;
        }
    }
}
