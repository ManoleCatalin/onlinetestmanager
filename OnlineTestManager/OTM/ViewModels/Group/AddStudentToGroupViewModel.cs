using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace OTM.ViewModels.Group
{
    public class AddStudentToGroupViewModel
    {
        [HiddenInput]
        public Guid GroupId { get; set; }
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }
    }
}
