using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Core.Domain
{
    public class Test
    {
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid TestTypeId { get; set; }
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
