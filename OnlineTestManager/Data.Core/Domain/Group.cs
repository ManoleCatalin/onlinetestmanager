using System;
using System.Collections.Generic;

namespace Data.Core.Domain
{
    public class Group
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public static Group Create(string name, string description, Guid userId)
        {
            var instance = new Group { Id = Guid.NewGuid(), CreatedAt = DateTime.Now};
            instance.Update(name, description, userId);
            return instance;
        }

        public void Update(string name, string description, Guid userId)
        {
            Name = name;
            Description = description;
            UserId = userId;
        }
    }
}
