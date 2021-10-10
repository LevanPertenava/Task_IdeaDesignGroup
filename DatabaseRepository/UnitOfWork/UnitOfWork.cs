using DatabaseRepository.DbRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private PersonsRepository _personsRepository;
        private OrganizationsRepository _organizationsRepository;
        private PersonOrganizationsRepository _personOrganizationsRepository;
        
        public PersonsRepository Persons
        {
            get
            {
                return _personsRepository ?? (_personsRepository = new());
            }
        }

        public OrganizationsRepository Organizations
        {
            get
            {
                return _organizationsRepository ?? (_organizationsRepository = new());
            }
        }

        public PersonOrganizationsRepository PersonToOrganizations
        {
            get
            {
                return _personOrganizationsRepository ?? (_personOrganizationsRepository = new());
            }
        }
    }
}
