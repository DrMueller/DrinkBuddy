using System.Diagnostics.CodeAnalysis;
using DrinkBuddy.Domain.Shared.Data.Querying;
using DrinkBuddy.Domain.Shared.Data.Tables.Base;

namespace DrinkBuddy.DataAccess.DbContexts.Contexts
{
    public interface IAppDbContext : IDisposable, IQueryBase
    {
        [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Same name as the one on the DbContext needed")]
        IDbSetProxy<TTable> DbSet<TTable>() where TTable : TableBase;

        public ValueTask<TTable?> FindAsync<TTable>(params object?[]? keyValues)
            where TTable : class;

        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}