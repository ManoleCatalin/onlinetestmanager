using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTM.Models.GroupViewModels
{
    public class DetailsStudentInGroup
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
    }

    public class DetailsGroupViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<DetailsStudentInGroup> Students { get; set; }
    }
}
