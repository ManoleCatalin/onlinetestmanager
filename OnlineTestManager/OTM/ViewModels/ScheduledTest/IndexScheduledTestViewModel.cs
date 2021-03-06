﻿using System;
using System.ComponentModel.DataAnnotations;

namespace OTM.ViewModels.ScheduledTest
{
    public class IndexScheduledTestViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Start time")]
        public DateTime StartDateTime { get; set; }
        [Display(Name = "Duration in mins")]
        public int Duration { get; set; }
        [Display(Name = "Group")]
        public string GroupName { get; set; }
        [Display(Name = "Test")]
        public string TestName { get; set; }
        public Guid GroupId { get; set; }
    }
}
