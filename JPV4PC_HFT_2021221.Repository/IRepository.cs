using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPV4PC_HFT_2021221.Repository
{
    public interface IRepository<T>  where T : class
    {
        T GetOne(int id);
        IQueryable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
    }
}
