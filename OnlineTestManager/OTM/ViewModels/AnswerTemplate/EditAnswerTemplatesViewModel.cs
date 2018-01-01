using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTM.ViewModels.AnswerTemplate
{
    public class EditAnswerTemplatesViewModel
    {
        public Guid AnswerTemplateId { get; set; }
        public Guid ExerciseTemplateId { get; set; }
        public Guid TestTemplateId { get; set; }

        public string Description { get; set; }
        public bool Correct { get; set; }
    }
}
