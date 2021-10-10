using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task_IdeaDesignGroup.Models
{
    public class OrganizationModel
    {
        public OrganizationModel()
        {
            Persons = new List<PersonModel>();
        }
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Organization")]
        public string OrganizationName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Activity { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<PersonModel> Persons { get; set; }
    }
}
