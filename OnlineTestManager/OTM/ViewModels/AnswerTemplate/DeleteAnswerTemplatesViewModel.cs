using System;
using System.ComponentModel.DataAnnotations;

namespace OTM.ViewModels.AnswerTemplate
{
    public class DeleteAnswerTemplatesViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Answer")]
        public string Description { get; set; }
        public Guid TestTemplateId { get; set; }
        public Guid ExerciseTemplateId { get; set; }
    }
}
