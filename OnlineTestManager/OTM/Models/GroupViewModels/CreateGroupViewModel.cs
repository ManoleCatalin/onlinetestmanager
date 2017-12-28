using System.ComponentModel.DataAnnotations;
using Constants;

namespace OTM.Models.GroupViewModels
{
    public class CreateGroupViewModel
    {
        [MaxLength(CoreConfigurationConstants.MaxLength)]
        public string Name { get; set; }

        [MaxLength(CoreConfigurationConstants.MaxLength)]
        public string Description { get; set; }
    }
}
