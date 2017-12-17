using System;

namespace Data.Core.Domain
{
    public class Test : IBaseEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid UserId { get; private set; }
        public virtual User User { get; set; }
        public Guid TestTypeId { get; private set; }
        public virtual TestType TestType { get; set; }

        public static Test Create(string name, string description, Guid userId, Guid testTypeId)
        {
            var instance = new Test { Id = Guid.NewGuid(), CreatedAt = DateTime.Now };
            instance.Update(name, description, userId, testTypeId);
            return instance;
        }

        public void Update(string name, string description, Guid userId, Guid testTypeId)
        {
            Name = name;
            Description = description;
            UserId = userId;
            TestTypeId = testTypeId;
        }
    }
}
