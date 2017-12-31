using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTM.Models.TestTemplatesViewModels
{
    public class IndexTestTemplatesViewModel
    {
        public Guid Id { get; set; }
        public string TestTypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
