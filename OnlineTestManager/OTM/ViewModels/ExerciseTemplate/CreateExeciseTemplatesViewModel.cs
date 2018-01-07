using System;
using System.ComponentModel.DataAnnotations;

namespace OTM.ViewModels.ExerciseTemplate
{
    public class CreateExerciseTemplatesViewModel
    {
        public Guid TestTemplateId { get; set; }
        [Display(Name = "Exercise")]
        public string Description { get; set; }
    }
}
