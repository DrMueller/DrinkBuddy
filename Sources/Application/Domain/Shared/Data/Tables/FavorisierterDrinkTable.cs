using DrinkBuddy.Domain.Shared.Data.Tables.Base;
using JetBrains.Annotations;

namespace DrinkBuddy.Domain.Shared.Data.Tables
{
    [PublicAPI("EF Core")]
    public class FavorisierterDrinkTable : TableBase
    {
        public string Name { get; set; } = null!;
        public virtual ProfilTable ProfilTable { get; set; } = null!;
        public int ProfilTableId { get; set; }
    }
}