using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Core.Domain
{
    public class TestInstance
    {
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string ConnectionToken { get; set; }

        public DateTime StartedAt { get; set; }

        public int Duration { get; set; }

        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }

        public Guid TestId { get; set; }
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
