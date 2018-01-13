using System;
using System.Collections.Generic;

namespace Data.Core.Domain
{
    public class TestInstance : IBaseEntity
    {
        public Guid Id { get; private set; }
        public DateTime StartedAt { get; private set; }
        public int Duration { get; private set; }
        public Guid GroupId { get; private set; }
        public virtual Group Group { get; set; }
        public Guid TestId { get; private set; }
        public virtual Test Test { get; set; }
        public ICollection<Grade> Grades { get; set; }

        public static TestInstance Create(int duration, Guid groupId, Guid testId, DateTime startedAt)
        {
            var instance = new TestInstance { Id = Guid.NewGuid() };
            instance.Update(duration, groupId, testId, startedAt);
            return instance;
        }

        public void Update(int duration, Guid groupId, Guid testId, DateTime startedAt)
        {
            Duration = duration;
            GroupId = groupId;
            TestId = testId;
            StartedAt = startedAt;
        }
    }
}