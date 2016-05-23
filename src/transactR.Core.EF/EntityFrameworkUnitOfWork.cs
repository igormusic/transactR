using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TransactRules.Core.Entities;

namespace transactR.Core.EF
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
       protected readonly DbContext Context;

        public EntityFrameworkUnitOfWork(DbContext context)
        {
            Context = context;
        }

        public void Create(IEntity obj)
        {
            Context.Add(obj);
        }


        private object GetGenericSet(Type ofType)
        {
            var setMethod = Context.GetType().GetMethod("Set");
            setMethod.MakeGenericMethod(new Type[] { ofType });
            var set = setMethod.Invoke(Context, new Object[] { });

            return set;
        }

        public IQuery CreateQuery(Type ofType)
        {

            var set = GetGenericSet(ofType);

            var genericType = typeof(EntityFrameworkQuery<>).MakeGenericType(new Type[] { ofType });

            var query = Activator.CreateInstance(genericType, new Object[] { set });

            return (IQuery)query;

        }

        public void Delete(IEntity obj)
        {
            Context.Remove(obj);
        }

        public IEntity GetById(Type type, int id)
        {
            var set = GetGenericSet(type);
            var findMethod = set.GetType().GetMethod("Find");
            var value =  findMethod.Invoke(set, new object[] { id });

            return (IEntity)value;
            // return Context.Set<TEntity>().Find(id);
        }

        public void Update(IEntity obj)
        {
            Context.Update(obj);
        }
    }
}
