﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseRepository
{
    public partial class Organizations
    {
        public Organizations()
        {
            PersonOrganizations = new HashSet<PersonOrganizations>();
        }

        public Guid Id { get; set; }
        public string OrganizationName { get; set; }
        public string Address { get; set; }
        public string Activity { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<PersonOrganizations> PersonOrganizations { get; set; }
    }
}