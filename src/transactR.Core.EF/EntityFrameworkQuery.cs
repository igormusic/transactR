using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using transactR.Core.Entities;

namespace transactR.Core.EF
{
    public class EntityFrameworkQuery<T> : IQuery<T> where T:class
    {
        protected readonly DbSet<T> DbSet;

        public EntityFrameworkQuery(DbSet<T> dbSet)
        {
            DbSet = dbSet;
        }

        public IQueryable<T> Items
        {
            get
            {
                return DbSet;
            }

        }
    }

}
