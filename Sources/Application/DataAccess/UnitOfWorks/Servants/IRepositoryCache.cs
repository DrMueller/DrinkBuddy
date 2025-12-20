using DrinkBuddy.DataAccess.DbContexts.Contexts;
using DrinkBuddy.Domain.Shared.Data.Writing;

namespace DrinkBuddy.DataAccess.UnitOfWorks.Servants
{
    public interface IRepositoryCache
    {
        TRepo GetRepository<TRepo>(IAppDbContext dbContext)
            where TRepo : IRepository;
    }
}