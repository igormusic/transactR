using System.Linq;


namespace transactR.Core.Entities
{
    public interface IQuery
    {

    }

    public interface IQuery<T> : IQuery
    {
        IQueryable<T> Items { get; }
    }
}