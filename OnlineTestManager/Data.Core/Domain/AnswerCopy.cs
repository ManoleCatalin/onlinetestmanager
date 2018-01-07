using System;

namespace Data.Core.Domain
{
    public class AnswerCopy
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public bool Correct { get; private set; }
        public Guid ExerciseCopyId { get; private set; }
        public virtual ExerciseCopy ExerciseCopy { get; set; }

        public static AnswerCopy Create(string description, bool correct, Guid exerciseCopyId)
        {
            var instance = new AnswerCopy { Id = Guid.NewGuid() };
            instance.Update(description, correct, exerciseCopyId);
            return instance;
        }

        public void Update(string description, bool correct, Guid exerciseCopyId)
        {
            Description = description;
            Correct = correct;
            ExerciseCopyId = exerciseCopyId;
        }
    }
}
