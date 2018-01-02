using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
