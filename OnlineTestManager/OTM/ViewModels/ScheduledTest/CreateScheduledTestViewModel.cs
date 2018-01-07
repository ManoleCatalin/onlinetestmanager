﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OTM.ViewModels.ScheduledTest
{
    public class CreateScheduledTestViewModel
    {
        public List<SelectListItem> Groups { get; set; }
        public List<SelectListItem> Tests { get; set; }
        public int Duration { get; set; }
        [Display(Name = "Start time")]
        [DataType(DataType.DateTime)]
        public DateTime StartDateTime { get; set; }
        public string Group { get; set; }
        public string Test { get; set; }
    }
}
