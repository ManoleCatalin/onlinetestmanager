using System;
using System.ComponentModel.DataAnnotations;
using Data.Core.Domain;

namespace OTM.ViewModels.TestTemplates
{
    public class DeleteTestTemplateViewModel
    {
        public Guid Id { get; set; }    
        [Display(Name = "Test Type")]
        public string TestTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TestType TestType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
