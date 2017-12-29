using System;
using System.Collections.Generic;

namespace OTM.Models.GroupViewModels
{
    public class EditStudentInGroup
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
    };

    public class EditGroupViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<EditStudentInGroup> Students { get; set; }
    }
}