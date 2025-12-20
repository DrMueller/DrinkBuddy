using DrinkBuddy.DataAccess.DbContexts.Contexts;

namespace DrinkBuddy.DataAccess.Repositories.Base
{
    public interface IRepositoryBase
    {
        internal void Initialize(IAppDbContext dbContext);
    }
}