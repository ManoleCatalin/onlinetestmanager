using System;

namespace Data.Core.Domain
{
    public class Answer
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public bool Correct { get; private set; }
        public Guid ExerciseId { get; private set; }
        public virtual Exercise Exercise { get; set; }

        public static Answer Create(string description, bool correct, Guid exerciseId)
        {
            var instance = new Answer { Id = Guid.NewGuid() };
            instance.Update(description, correct, exerciseId);
            return instance;
        }

        public void Update(string description, bool correct, Guid exerciseId)
        {
            Description = description;
            Correct = correct;
            ExerciseId = exerciseId;
        }

    }
}
