using System.Linq;


namespace TransactRules.Core.Entities
{
    public interface IQuery
    {

    }

    public interface IQuery<T> : IQuery
    {
        IQueryable<T> Items { get; }
    }
}