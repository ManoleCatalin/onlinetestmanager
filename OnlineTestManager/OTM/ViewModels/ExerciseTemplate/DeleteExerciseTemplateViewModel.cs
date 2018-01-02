using System;
using System.ComponentModel.DataAnnotations;

namespace OTM.ViewModels.ExerciseTemplate
{
    public class DeleteExerciseTemplateViewModel
    {
        public Guid Id { get; set; }
        public Guid TestTemplateId { get; set; }
        [Display(Name = "Exercise")]
        public string Description { get; set; }
    }
}
