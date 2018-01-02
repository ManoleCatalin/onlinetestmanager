using System;

namespace OTM.ViewModels.Group
{
    public class DeleteGroupViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
