using DatabaseRepository.DbRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseRepository
{
    public interface IUnitOfWork
    {
        PersonsRepository Persons { get; }

        OrganizationsRepository Organizations { get; }

        PersonOrganizationsRepository PersonToOrganizations { get; }
    }
}
