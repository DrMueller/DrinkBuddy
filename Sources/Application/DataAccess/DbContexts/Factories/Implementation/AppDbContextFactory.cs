using DrinkBuddy.Common.Settings.Provisioning.Services;
using DrinkBuddy.DataAccess.DbContexts.Contexts;
using DrinkBuddy.DataAccess.DbContexts.Contexts.Implementation;
using Microsoft.EntityFrameworkCore;

namespace DrinkBuddy.DataAccess.DbContexts.Factories.Implementation
{
    public class AppDbContextFactory(
        IDbContextOptionsFactory optionsFactory,
        ISettingsProvider appSettingsProvider)
        : IAppDbContextFactory
    {
        private readonly Lazy<DbContextOptions> _lazyOptions = new(() => optionsFactory
            .CreateForSqlServer(appSettingsProvider.AppSettings.ConnectionString)
        );

        public IAppDbContext Create()
        {
            return new AppDbContext(_lazyOptions.Value);
        }
    }
}