using DatabaseRepository.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseRepository.DbRepositories
{
    public class PersonsRepository : BaseRepository<Persons>
    {
        private AppDbContext _context;
        public PersonsRepository() : base()
        {
        }
        
        public override IEnumerable<Persons> Get()
        {
            using (_context = new AppDbContext())
            {
                var query = (from p in _context.Persons where p.IsActive == true select p).ToList();
                return query;
            }
        }

        public override Persons GetById(object id)
        {
            using (_context = new AppDbContext())
            {
                var person = (from p in _context.Persons where p.IsActive == true && p.Id == (Guid)id select p).FirstOrDefault();
                return person;
            }
        }


        public override void Delete(Persons entity)
        {
            if (entity.IsActive is false)
            {
                throw new Exception("Can't delete unregistered person");
            }
            using (_context = new AppDbContext())
            {
                entity.IsActive = false;
                _context.Persons.Update(entity);
                _context.SaveChanges();
            }
        }

        public async Task<Persons> FindByPersonalNumberAsync(string personalNumber)
        {
            using (_context = new AppDbContext())
            {
                var query = await (from p in _context.Persons
                                   where p.PersonalNumber == personalNumber
                                   select p).SingleOrDefaultAsync();
                return query;
            }
        }

        public async Task<IEnumerable<Persons>> SearchPerson(string searchText)
        {
            using (_context = new AppDbContext())
            {
               return await _context.Persons.FromSqlRaw("SP_SearchPerson {0}", searchText)
                    .ToListAsync();
            }
        }
    }
}
