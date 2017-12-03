using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Core.Domain
{
    public class User
    {
        public  Guid Id { get; set; }
        [MaxLength(255)]
        public String FirstName { get; set; }
        [MaxLength(255)]
        public String LastName { get; set; }
        [MaxLength(255)]
        public String Email { get; set; }
        [MaxLength(255)]
        public String Password { get; set; }
        public Guid? UserTypeId { get; set; }
        public virtual UserType UserType { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        //public Guid? GroupId { get; set; }
        //public virtual Group Group{ get; set; }
    }
}
