﻿using System;
using System.Collections.Generic;

namespace Data.Core.Domain
{
    public class Group : IBaseEntity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ICollection<UserGroup> UserGroups { get; set; }
        public Guid UserId { get; private set; }
        public virtual User User { get; set; }
		public ICollection<TestInstance> TestInstances { get; set; }
        public bool IsDeleted { get; set; }
        public static Group Create(string name, string description, Guid userId)
        {
            var instance = new Group { Id = Guid.NewGuid(), CreatedAt = DateTime.Now};
            instance.Update(name, description, userId,false);
            return instance;
        }

        public void Update(string name, string description, Guid userId,bool isDeleted)
        {
            Name = name;
            Description = description;
            UserId = userId;
            IsDeleted = isDeleted;
        }
    }
}
