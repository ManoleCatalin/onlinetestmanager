using System;
using System.Collections.Generic;

namespace OTM.ViewModels.ExerciseTemplate
{
    public class EditAnswer
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool Correct { get; set; }
    }


    public class EditExerciseTemplatesViewModel
    {
        public Guid TestTemplateId { get; set; }
        public Guid Id { get; set; }
        public string Description { get; set; }
        public List<EditAnswer> Answers { get; set; }
    }
}
