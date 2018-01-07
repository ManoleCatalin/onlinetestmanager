using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Data.Core.Domain;

namespace OTM.ViewModels.ScheduledTest
{
    public class ScheduledTestDetailsExercise
    {
        public string Description { get; set; }
        public List<Answer> Answers { get; set; }
    }
    public class ScheduledTestDetailsTest
    {
        public string TestType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ScheduledTestDetailsExercise> Exercises { get; set; }
    }

    public class ScheduledTestDetailsGroup
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<User> Users { get; set; }
    }
    public class DetailsScheduledTestViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Test")]
        public ScheduledTestDetailsTest TestDetails { get; set; }
        [Display(Name = "Students")]
        public ScheduledTestDetailsGroup GroupDetails { get; set; }
    }
}
