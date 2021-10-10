using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseRepository.DbRepositories
{
    public class PersonOrganizationsRepository
    {
        private AppDbContext _context;

        public void InsertPersonToOrganization(Guid personId, Guid organizationId)
        {
            try
            {
            using (_context = new AppDbContext())
            {
                _context.Database.ExecuteSqlInterpolated($"AssignCategoryToPerson_SP {personId}, {organizationId}");
            }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                
            }

        }

        public void RemovePersonFromOrganization(Guid personId, Guid organizationId)
        {
            using (_context = new AppDbContext())
            {
                _context.Database.ExecuteSqlInterpolated($"UnAssignCategoryToPerson_SP {personId}, {organizationId}");
            }
        }
    }
}
