using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OTM.ViewModels.ScheduledTest
{
    public class IndexScheduledTestViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Start time")]
        public DateTime StartDateTime { get; set; }
        public int Duration { get; set; }
        [Display(Name = "Group")]
        public string GroupName { get; set; }
        [Display(Name = "Test")]
        public string TestName { get; set; }
        public Guid GroupId { get; set; }
    }
}
