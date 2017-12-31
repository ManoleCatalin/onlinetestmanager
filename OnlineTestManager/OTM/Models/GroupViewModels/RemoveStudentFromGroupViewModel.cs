using System;
using System.ComponentModel.DataAnnotations;

namespace OTM.Models.GroupViewModels
{
    public class RemoveStudentFromGroupViewModel
    {
        public Guid GroupId { get; set; }
        public Guid StudentId { get; set; }
        [Display(Name="Student Name")]
        public string StudentName { get; set; }
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }
    }
}
