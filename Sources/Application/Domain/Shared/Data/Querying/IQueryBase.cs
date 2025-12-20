using DrinkBuddy.Data.Base;

namespace DrinkBuddy.Domain.Shared.Data.Querying
{
    public interface IQueryBase
    {
        IQueryable<T> Query<T>()
            where T : TableBase;
    }
}