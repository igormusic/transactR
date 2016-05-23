using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransactRules.Core.Entities
{

    public interface IRepository<T> where T : IEntity
    {
        void Create(T obj);
        T GetById(int id);
        void Update(T obj);
        void Delete(T obj);
        IQueryable<T> Items();
    }
}
