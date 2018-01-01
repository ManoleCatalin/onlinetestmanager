using System;
using System.Collections.Generic;

namespace OTM.ViewModels.TestTemplates
{
    public class EditExercise
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }

    public class EditTestTemplatesViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<EditExercise> Exercises { get; set; }
    }
}
