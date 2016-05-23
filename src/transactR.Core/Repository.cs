using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace transactR.Core.Entities
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        private IUnitOfWork unitOfWork;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return unitOfWork;
            }

            set
            {
                unitOfWork = value;
            }
        }

        public virtual void Create(T obj)
        {
            UnitOfWork.Create(obj);
        }

        public virtual T GetById(int id)
        {
            return (T)UnitOfWork.GetById(typeof(T), id);
        }

        public virtual void Update(T obj)
        {
            UnitOfWork.Update(obj);
        }

        public virtual void Delete(T obj)
        {
            UnitOfWork.Delete(obj);
        }

        public virtual IQueryable<T> Items()
        {
            var query = (IQuery<T>)UnitOfWork.CreateQuery(typeof(T));

            return query.Items;
        }
    }
}
