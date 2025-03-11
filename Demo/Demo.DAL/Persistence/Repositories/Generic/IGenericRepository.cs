using Demo.DAL.Entities;
using Demo.DAL.Entities.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Persistence.Repositories.Generic
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        public ICollection<T> GetAll(bool asNoTracking = true);


        public T? GetById(int departmentId);

        public void AddT(T Entity);


        public void UpdateT(T Entity);

        public void DeleteT(T Entity);


        public IQueryable<T> GetAllQueryable();
        //public IEnumerable <T> GetAllEnumrable();

       
        


    }
}
