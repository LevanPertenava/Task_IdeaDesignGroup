using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Task_IdeaDesignGroup.Utility;

namespace Task_IdeaDesignGroup.Models
{
    public class PersonModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "Lastname cannot exceed 50 characters")]
        public string LastName { get; set; }

        [Required]
        public Gender? Gender { get; set; }

        [Required]

        [Display(Name = "Personal ID")]
        [ValidPersonalId(8, ErrorMessage = "Invalid ID")]
        public string PersonalNumber { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [Phone(ErrorMessage = "Incorrect phone number")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
