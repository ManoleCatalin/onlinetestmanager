using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Core.Domain
{
    public class User
    {
        public  Guid Id { get; set; }
        [MaxLength(255)]
        public string FirstName { get; set; }
        [MaxLength(255)]
        public string LastName { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(255)]
        public string PasswordHash { get; set; }
        [Required]
        public Guid UserTypeId { get; set; }
        public virtual UserType UserType { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }

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
