using System;
using System.Collections.Generic;

namespace OTM.ViewModels.Tests
{
    public class MarkedCorrectAnswerDisplayTestsViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool Correct { get; set; }
    }
    public class DisplayTestsViewModel
    {
        public Guid UserId { get; set; }
        public Guid TestInstanceId { get; set; }
        public int Duration { get; set; }
        public DateTime StaDateTime { get; set; }
        public Guid ExerciseId { get; set; }
        public string Description { get; set; }
        public List<MarkedCorrectAnswerDisplayTestsViewModel> Answers { get; set; }
        public int CorrectAnswers { get; set;}
    }
}
