using Demo.DAL.Entities;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Persistence.Data;
using Demo.DAL.Persistence.Repositories.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Persistence.Repositories.Generic
{
    public class GenericRepository <T> : IGenericRepository<T> where T : ModelBase
    {
        private readonly ApplicationDbContext _dbContext;
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public ICollection<T> GetAll(bool asNoTracking = true)
        {
            if (asNoTracking)
            {

                _dbContext.Set<T>().AsNoTracking().ToList();

            }
            return _dbContext.Set<T>().ToList();
        }

        public T? GetById(int departmentId)
        {
            //return _dbContext.Departments.Local.FirstOrDefault(d => d.Id == departmentId);
            return _dbContext.Set<T>().Find(departmentId); // search locally first 
        }
        public int AddT(T Entity)
        {
            _dbContext.Set<T>().Add(Entity);
            return _dbContext.SaveChanges();
        }

        public int UpdateT(T Entity)
        {
            _dbContext.Set<T>().Update(Entity);
            return _dbContext.SaveChanges();
        }
        public int DeleteT(T Entity)
        {
            _dbContext.Set<T>().Remove(Entity);
            return _dbContext.SaveChanges();

        }


        public IQueryable<T> GetAllQueryable()
        {
            return _dbContext.Set<T>();
        }

    }
}
