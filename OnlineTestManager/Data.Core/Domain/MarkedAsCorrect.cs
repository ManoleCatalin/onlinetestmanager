
using System;

namespace Data.Core.Domain
{
    public class MarkedAsCorrect
    {
        public Guid AnswerCopyId { get; set; }
        public virtual AnswerCopy AnswerCopy { get; set; }
        public Guid ExerciseResponseId { get; set; }

        public Guid ExerciseCopyId { get; set; }
        public Guid TestInstanceId { get; set; }
        public Guid UserId { get; set; }

        public ExerciseResponse ExerciseResponse{ get; set; }
    }
}
