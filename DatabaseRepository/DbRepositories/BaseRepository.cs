using DatabaseRepository.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Utility;

namespace DatabaseRepository.DbRepositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private AppDbContext _context;

        public virtual void Delete(TEntity entity)
        {
            try
            {
                using (_context = new AppDbContext())
                {
                    using (TransactionScope transaction = new TransactionScope())
                    {
                        _context.Set<TEntity>().Remove(entity);
                        _context.SaveChanges();
                        transaction.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public virtual IEnumerable<TEntity> Get()
        {
            using (_context = new AppDbContext())
            {
                return _context.Set<TEntity>().ToList();
            }
        }

        public virtual TEntity GetById(object id)
        {
            using (_context = new AppDbContext())
            {
                return _context.Set<TEntity>().Find(id);
            }
        }

        public virtual void Insert(TEntity entity)
        {
            try
            {
                using (_context = new AppDbContext())
                {
                    using (TransactionScope transaction = new TransactionScope())
                    {
                        _context.Set<TEntity>().Add(entity);
                        _context.SaveChanges();
                        transaction.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public virtual void Update(TEntity entity)
        {
            try
            {
                using (_context = new AppDbContext())
                {
                    using (TransactionScope transaction = new TransactionScope())
                    {
                        _context.Set<TEntity>().Update(entity);
                        _context.SaveChanges();
                        transaction.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
