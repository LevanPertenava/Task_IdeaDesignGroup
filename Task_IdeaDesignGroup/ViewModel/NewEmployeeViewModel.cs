using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_IdeaDesignGroup.Models;

namespace Task_IdeaDesignGroup.ViewModel
{
    public class NewEmployeeViewModel
    {
        public NewEmployeeViewModel()
        {
            Persons = new List<PersonModel>();
        }
        public Guid OrganizationId { get; set; }

        public ICollection<PersonModel> Persons { get; set; }
    }
}
