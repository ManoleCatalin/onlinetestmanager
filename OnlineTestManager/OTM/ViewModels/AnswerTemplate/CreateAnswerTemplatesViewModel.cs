using System;
using System.ComponentModel.DataAnnotations;

namespace OTM.ViewModels.AnswerTemplate
{
    public class CreateAnswerTemplatesViewModel
    {
        public Guid TestTemplateId { get; set; }
        public Guid ExerciseTemplateId { get; set; }
        [Display(Name = "Answer")]
        public string Description { get; set; }
        public bool Correct { get; set; }
    }
}
