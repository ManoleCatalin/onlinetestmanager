using System;
using System.Collections.Generic;

namespace OTM.ViewModels.ExerciseTemplate
{
    public class IndexExercise
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class IndexExerciseTemplatesViewModel
    {
        public Guid TestTemplateId { get; set; }
        public List<IndexExercise> IndexExercises { get; set; }
    }
}
