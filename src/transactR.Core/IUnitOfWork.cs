using System;


namespace transactR.Core.Entities
{
    public interface IUnitOfWork
    {
        void Create(IEntity obj);
        IEntity GetById(Type type, int id);
        void Update(IEntity obj);
        void Delete(IEntity obj);

        IQuery CreateQuery(Type ofType);

        //Type GetClass(IEntity obj);
    }
}
