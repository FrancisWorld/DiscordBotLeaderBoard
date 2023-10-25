using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(ulong id);

        void Save(T entity);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetAllById(ulong id);

        void Update(T entity);
    }
}