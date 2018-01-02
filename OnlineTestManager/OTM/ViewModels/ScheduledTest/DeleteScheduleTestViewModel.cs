using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OTM.ViewModels.ScheduledTest
{
    public class DeleteScheduleTestViewModel
    {
        public Guid Id { get; set; }
        public string Group { get; set; }
        public string Test { get; set; }
        public int Duration { get; set; }
        [Display(Name = "Start time")]
        public DateTime StartDateTime { get; set; }
    }
}
