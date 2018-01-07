using System;

namespace Data.Core.Domain
{
    public class UserGroupCopy
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public GroupCopy GroupCopy { get; set; }
        public Guid GroupCopyId { get; set; }

        public static UserGroupCopy Create(Guid userId, Guid groupCopyId)
        {
            return new UserGroupCopy { UserId = userId, GroupCopyId = groupCopyId };
        }
    }
}
