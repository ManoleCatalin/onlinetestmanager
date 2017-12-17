using System;
using System.Collections.Generic;

namespace Data.Core.Domain
{
    public class TestInstance : IBaseEntity
    {
        public Guid Id { get; private set; }
        public string ConnectionToken { get; private set; }
        public DateTime StartedAt { get; private set; }
        public int Duration { get; private set; }
        public Guid GroupId { get; private set; }
        public virtual Group Group { get; set; }
        public Guid TestId { get; private set; }
        public virtual Test Test { get; set; }
        public ICollection<Grade> Grades { get; set; }

        public static TestInstance Create(string connectionToken,int duration, Guid groupId, Guid testId)
        {
            var instance = new TestInstance { Id = Guid.NewGuid(), StartedAt = DateTime.Now };
            instance.Update(connectionToken,duration, groupId, testId);
            return instance;
        }

        public void Update(string connectionToken,int duration, Guid groupId, Guid testId)
        {
            ConnectionToken = connectionToken;
            Duration = duration;
            GroupId = groupId;
            TestId = testId;
        }
    }
}
