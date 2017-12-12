using System;
using System.Collections.Generic;

namespace Data.Core.Domain
{
    public class Exercise
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public Guid TestId { get; private set; }
        public virtual Test Test { get; set; }
        public ICollection<Answer> Answers { get; set; }

        public static Exercise Create(string description, Guid testId)
        {
            var instance = new Exercise { Id = Guid.NewGuid()};
            instance.Update(description, testId);
            return instance;
        }

        public void Update(string description, Guid testId)
        {
            Description = description;
            TestId = testId;
        }
    }
}
