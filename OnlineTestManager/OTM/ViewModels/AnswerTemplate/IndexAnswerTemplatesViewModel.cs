using System;
using System.Collections.Generic;
using Data.Core.Domain;

namespace OTM.ViewModels.AnswerTemplate
{
    public class IndexAnswer
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool Correct { get; set; }
    }

    public class IndexAnswerTemplatesViewModel
    {
        public Guid TestTemplateId { get; set; }
        public Guid ExerciseTemplateId { get; set; }
        public List<IndexAnswer> Answers { get; set; }
    }
}
