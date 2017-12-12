using System;

namespace Data.Core.Domain
{
    public class UserGroup 
    {
        public User User { get; set; }      
        public Guid UserId { get; set; }
        public Group Group { get; set; }
        public Guid GroupId { get; set; }

        public static UserGroup Create(Guid userId, Guid groupId)
        {
            return new UserGroup { UserId = userId, GroupId = groupId };
        }
    }
}
