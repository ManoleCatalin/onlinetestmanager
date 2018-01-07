using System;
using System.Collections.Generic;

namespace Data.Core.Domain
{
    public class ExerciseCopy
    {
        public Guid Id { get; set; }
        public Guid TestInstanceId { get; set; }
        public string Description { get; set; }
        public virtual TestInstance TestInstance { get; set; }
        public ICollection<AnswerCopy> AnswersCopies { get; set; }
        public ICollection<ExerciseResponse> ExerciseResponses { get; set; }

        public static ExerciseCopy Create(Guid testInstanceId, string description)
        {
            var instance = new ExerciseCopy{Id = Guid.Empty};
            instance.Update(testInstanceId, description);
            return instance;
        }

        public void Update(Guid testInstaceId, string description)
        {
            TestInstanceId = testInstaceId;
            Description = description;
        }
    }
}
