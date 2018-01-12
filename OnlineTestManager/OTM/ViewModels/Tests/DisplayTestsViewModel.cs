using System;
using System.Collections.Generic;

namespace OTM.ViewModels.Tests
{
    public class AnswerDisplayTestsViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool Correct { get; set; }
    }
    public class DisplayTestsViewModel
    {
        public Guid TestInstanceId { get; set; }
        public Guid ExerciseId { get; set; }
        public string Description { get; set; }
        public List<AnswerDisplayTestsViewModel> Answers { get; set; }
    }
}
