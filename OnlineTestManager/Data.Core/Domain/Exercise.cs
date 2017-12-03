using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Core.Domain
{
    public class Exercise
    {
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public Guid TestId { get; set; }
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
