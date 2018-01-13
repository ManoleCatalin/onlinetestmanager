using System;
using System.ComponentModel.DataAnnotations;

namespace OTM.ViewModels.ScheduledTest
{
    public class DeleteScheduleTestViewModel
    {
        public Guid Id { get; set; }
        public string Group { get; set; }
        public string Test { get; set; }
        [Display(Name = "Duration in mins")]
        public int Duration { get; set; }
        [Display(Name = "Start time")]
        public DateTime StartDateTime { get; set; }
    }
}
