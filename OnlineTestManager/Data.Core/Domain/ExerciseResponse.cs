using System;
using System.Collections.Generic;

namespace Data.Core.Domain
{
    public class ExerciseResponse
    {
        public Guid ExerciseCopyId { get; set; }
        public Guid TestInstanceId { get; set; }
        public Guid UserId { get; set; }
        public virtual  ExerciseCopy ExerciseCopy { get; set; }
        public virtual TestInstance TestInstance { get; set; }
        public virtual User User { get; set; }

        public ICollection<MarkedAsCorrect> MarkedAsCorrects { get; set; }
    }
}
