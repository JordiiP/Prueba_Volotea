using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);

        void Delete(T entity);

        T Get(int id);

        IList<T> GetAll();
    }
}
