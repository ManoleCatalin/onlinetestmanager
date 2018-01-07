using System;
using System.Collections.Generic;

namespace Data.Core.Domain
{
    public class GroupCopy
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ICollection<UserGroupCopy> UserGroupsCopies { get; set; }
        public Guid TestInstanceId { get; set; }
        public virtual TestInstance TestInstance { get; set; }

        public static GroupCopy Create(string name, string description, Guid testInstanceId)
        {
            var instance = new GroupCopy { Id = Guid.NewGuid()};
            instance.Update(name, description, testInstanceId);
            return instance;
        }

        public void Update(string name, string description, Guid testInstanceId)
        {
            Name = name;
            Description = description;
            TestInstanceId = testInstanceId;
        }
    }
}
