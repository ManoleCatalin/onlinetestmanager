using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OTM.Models.TestTemplatesViewModels
{
    public class CreateTestTemplatesViewModel
    {
        [Display(Name = "Test Type")]
        public string TestTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SelectListItem> TestTypes { get; set; }
    }
}
