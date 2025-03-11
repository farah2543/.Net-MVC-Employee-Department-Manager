using Demo.DAL.Persistence.Data;
using Demo.DAL.Persistence.Repositories.Departments;
using Demo.DAL.Persistence.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        public ApplicationDbContext _dbContext { get; }
        public IEmployeeRepository EmployeeRepository => new EmployeeRepository(_dbContext);
        public IDepartmentRepository DepartmentRepository =>new DepartmentRepository(_dbContext);

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            //EmployeeRepository = new EmployeeRepository(dbContext);
            //DepartmentRepository = new DepartmentRepository(dbContext);
        }
      

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Dispose();   

        }
    }
}
