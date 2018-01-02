using System;

namespace OTM.ViewModels.AnswerTemplate
{
    public class CreateAnswerTemplatesViewModel
    {
        public Guid TestTemplateId { get; set; }
        public Guid ExerciseTemplateId { get; set; }
        public string Description { get; set; }
        public bool Correct { get; set; }
    }
}
