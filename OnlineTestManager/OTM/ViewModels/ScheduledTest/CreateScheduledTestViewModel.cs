﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OTM.ViewModels.ScheduledTest
{
    public class CreateScheduledTestViewModel
    {
        public List<SelectListItem> Groups { get; set; }
        public List<SelectListItem> Tests { get; set; }
        [Display(Name = "Duration in mins")]
        public int Duration { get; set; }
        [Display(Name = "Start time")]
        [DataType(DataType.DateTime)]
        public DateTime StartDateTime { get; set; }
        public string Group { get; set; }
        public string Test { get; set; }

        public CreateScheduledTestViewModel()
        {
            DateTime currentDateTime = DateTime.Now;
                currentDateTime = new DateTime(
                currentDateTime.Ticks - (currentDateTime.Ticks % TimeSpan.TicksPerMinute),
                currentDateTime.Kind
            );

            StartDateTime = currentDateTime;
        }
    }
}
