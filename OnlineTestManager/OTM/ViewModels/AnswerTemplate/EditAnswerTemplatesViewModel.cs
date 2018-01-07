using System;
using System.ComponentModel.DataAnnotations;

namespace OTM.ViewModels.AnswerTemplate
{
    public class EditAnswerTemplatesViewModel
    {
        public Guid AnswerTemplateId { get; set; }
        public Guid ExerciseTemplateId { get; set; }
        public Guid TestTemplateId { get; set; }
        [Display(Name = "Answer")]
        public string Description { get; set; }
        public bool Correct { get; set; }
    }
}
