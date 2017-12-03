using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Core.Domain
{
    public class Answer
    {
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
        public bool Correct { get; set; }
        public Guid ExerciseId { get; set; }
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
