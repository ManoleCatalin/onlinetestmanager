using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTM.ViewModels.ExerciseTemplate
{
    public class DeleteExerciseTemplateViewModel
    {
        public Guid Id { get; set; }
        public Guid TestTemplateId { get; set; }
        public string Description { get; set; }
    }
}
