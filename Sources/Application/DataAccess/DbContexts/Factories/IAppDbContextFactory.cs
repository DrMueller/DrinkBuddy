using DrinkBuddy.DataAccess.DbContexts.Contexts;

namespace DrinkBuddy.DataAccess.DbContexts.Factories
{
    public interface IAppDbContextFactory
    {
        IAppDbContext Create();
    }
}