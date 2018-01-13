using System;

namespace OTM.ViewModels.Tests
{
    public class IndexTestsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public bool Ongoing { get; set; }
    }
}
