using DatabaseRepository.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseRepository.DbRepositories
{
    public class OrganizationsRepository : BaseRepository<Organizations>
    {
        private AppDbContext _context;
        public OrganizationsRepository() : base()
        {
        }

        public override IEnumerable<Organizations> Get()
        {
            using (_context = new AppDbContext())
            {
                var query = (from o in _context.Organizations where o.IsActive == true select o).ToList();
                return query;
            }
        }
        public override Organizations GetById(object id)
        {
            using (_context = new AppDbContext())
            {
                var organization = (from o in _context.Organizations where o.IsActive == true && o.Id == (Guid)id select o).FirstOrDefault();
                return organization;
            }
        }

        public override void Delete(Organizations entity)
        {
            if (entity.IsActive is false)
            {
                throw new Exception("Can't delete unregistered organization");
            }
            using (_context = new AppDbContext())
            {
                var query = (from o in _context.PersonOrganizations where o.OrganizationId == entity.Id select o).SingleOrDefault();

                if (query is not null)
                {
                    throw new Exception("Can't delete organization. Remove employees first");
                }
                entity.IsActive = false;
                _context.Organizations.Update(entity);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<Organizations>> SearchOrganization(string searchText)
        {
            using (_context = new AppDbContext())
            {
                return await _context.Organizations.FromSqlRaw("SP_SearchOrganization {0}", searchText)
                     .ToListAsync();
            }
        }

        public async Task<IEnumerable<Persons>> GetPersonsInOrganization(Guid organizationId)
        {
            using (_context = new AppDbContext())
            {
                return await _context.Persons.FromSqlRaw("SelectPersonsInOrganization_SP {0}", organizationId)
                      .ToListAsync();
            }
        }

        public async Task<IEnumerable<Persons>> GetPersonsOutOfOrganization(Guid organizationId)
        {
            using (_context = new AppDbContext())
            {
                return await _context.Persons.FromSqlRaw("SelectAllPersonsOutOfOrganization_SP {0}", organizationId)
                      .ToListAsync();
            }
        }
    }
}
