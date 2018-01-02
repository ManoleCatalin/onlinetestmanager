using System;

namespace OTM.ViewModels.AnswerTemplate
{
    public class DeleteAnswerTemplatesViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid TestTemplateId { get; set; }
        public Guid ExerciseTemplateId { get; set; }
    }
}
