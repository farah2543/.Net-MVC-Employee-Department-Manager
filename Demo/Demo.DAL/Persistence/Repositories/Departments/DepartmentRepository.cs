using Demo.DAL.Entities.Departments;
using Demo.DAL.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Persistence.Repositories.Departments
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        public ICollection<Department> GetAll(bool asNoTracking = true)
        {
            if (asNoTracking)
            {

                _dbContext.Departments.AsNoTracking().ToList();

            }
            return _dbContext.Departments.ToList();
        }

        public Department? GetById(int departmentId)
        {
            //return _dbContext.Departments.Local.FirstOrDefault(d => d.Id == departmentId);
            return _dbContext.Departments.Find(departmentId); // search locally first 
        }
        public int AddDepartment(Department Entity)
        {
            _dbContext.Departments.Add(Entity);
            return _dbContext.SaveChanges();
        }

        public int UpdateDepartment(Department Entity)
        {
            _dbContext.Departments.Update(Entity);
            return _dbContext.SaveChanges();
        }
        public int DeleteDepartment(Department Entity)
        {
            _dbContext.Departments.Remove(Entity);
            return _dbContext.SaveChanges();
           
        }

        IEnumerable<Department> IDepartmentRepository.GetAll(bool asNoTracking)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Department> GetAllQuerable()
        {
            return _dbContext.Departments;
        }
    }
}
