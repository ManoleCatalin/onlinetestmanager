
using System;

namespace Data.Core.Domain
{
    public class MarkedAsCorrect
    {
        public Guid AnswerId { get; set; }
        public virtual Answer Answer { get; set; }
        public Guid ExerciseResponseId { get; set; }

        public Guid ExerciseId { get; set; }
        public Guid TestInstanceId { get; set; }
        public Guid UserId { get; set; }

        public ExerciseResponse ExerciseResponse{ get; set; }
    }
}
