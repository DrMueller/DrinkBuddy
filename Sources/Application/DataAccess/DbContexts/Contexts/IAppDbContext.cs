using System.Diagnostics.CodeAnalysis;
using DrinkBuddy.Data.Base;
using DrinkBuddy.Domain.Shared.Data.Querying;

namespace DrinkBuddy.DataAccess.DbContexts.Contexts
{
    public interface IAppDbContext : IDisposable, IQueryBase
    {
        [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Same name as the one on the DbContext needed")]
        IDbSetProxy<TTable> DbSet<TTable>() where TTable : TableBase;

        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}