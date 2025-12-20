using Microsoft.EntityFrameworkCore;

namespace DrinkBuddy.DataAccess.DbContexts.Factories
{
    public interface IDbContextOptionsFactory
    {
        DbContextOptions CreateForSqlServer(string connectionString);
    }
}