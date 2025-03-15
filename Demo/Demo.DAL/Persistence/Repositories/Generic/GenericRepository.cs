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
        public async Task<ICollection<T>> GetAllAsync(bool asNoTracking = true)
        {
            if (asNoTracking)
            {

                await _dbContext.Set<T>().Where(X=>!X.IsDeleted).AsNoTracking().ToListAsync();

            }
            return await _dbContext.Set<T>().Where(X => !X.IsDeleted).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int departmentId)
        {
            //return _dbContext.Departments.Local.FirstOrDefault(d => d.Id == departmentId);
            return await _dbContext.Set<T>().FindAsync(departmentId); // search locally first 
        }

        public void AddT(T Entity)
        {
            _dbContext.Set<T>().Add(Entity);
        }

        public void UpdateT(T Entity)
        {
            _dbContext.Set<T>().Update(Entity);
        }
        public void DeleteT(T Entity)
        {
            //_dbContext.Set<T>().Remove(Entity);
            //return _dbContext.SaveChanges();

            Entity.IsDeleted = true;
            _dbContext.Set<T>().Update(Entity);

        }


        public IQueryable<T> GetAllQueryable()
        {
            return _dbContext.Set<T>();
        }

        //public IEnumerable<T> GetAllEnumrable()
        //{
        //    return _dbContext.Set<T>();
        //}
    }
}
