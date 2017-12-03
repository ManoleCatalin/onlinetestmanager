using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Core.Domain
{
    public class Group
    {
        public Guid Id { get; set; }
        [MaxLength(255)]
        public String Name { get; set; }
        [MaxLength(255)]
        public String Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
