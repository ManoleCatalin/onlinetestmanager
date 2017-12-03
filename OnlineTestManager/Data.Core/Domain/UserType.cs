using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Core.Domain
{
    public class UserType
    {
        public Guid Id { get; set; }
        [MaxLength(255)]
        public String Type { get; set; }
    }
}
