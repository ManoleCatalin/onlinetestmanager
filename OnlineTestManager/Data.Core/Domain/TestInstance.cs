using System;
using System.Collections.Generic;

namespace Data.Core.Domain
{
    public class TestInstance : IBaseEntity
    {
        public Guid Id { get; private set; }
        public DateTime StartedAt { get; private set; }
        public int Duration { get; private set; }

        public Guid TestId { get; private set; }
        public virtual Test Test { get; set; }

        public ICollection<Grade> Grades { get; set; }

        public Guid GroupId { get; private set; }
        public virtual Group Group { get; set; }

        public Guid GroupCopyId { get; private set; }
        public GroupCopy GroupCopy { get; set; }

        public Guid OwnerId { get; private set; }
        public string TestDescription { get; private set; }
        public string TestName { get; private set; }

        public  ICollection<ExerciseResponse> ExerciseResponses { get; set; }
        public ICollection<ExerciseCopy> ExerciseCopies { get; set; }

        public static TestInstance Create(int duration, Guid groupId, Guid testId, DateTime startedAt, 
            Guid ownerId, string groupName, string groupDescription, string testName, string testDescription)
        {
            var groupCopy = GroupCopy.Create(groupName, groupDescription, Guid.Empty);
            var testInstance = new TestInstance { Id = Guid.NewGuid(), OwnerId = ownerId, GroupCopyId = groupCopy.Id, GroupCopy = groupCopy};
            groupCopy.Update(groupName, groupDescription, testInstance.Id);
            testInstance.Update(duration, groupId, testId,startedAt, testName, testDescription, groupCopy.Id);

            return testInstance;
        }

        public void Update(int duration, Guid groupId, Guid testId, DateTime startedAt, string testName, string testDescription, Guid groupCopyId)
        {
            Duration = duration;
            GroupId = groupId;
            TestId = testId;
            StartedAt = startedAt;
            TestName = testName;
            TestDescription = testDescription;
            GroupCopyId = groupCopyId;
        }
    }
}
