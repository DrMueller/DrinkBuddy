using DrinkBuddy.Domain.Shared.Data.Tables.Base;

namespace DrinkBuddy.Domain.Shared.Data.Tables
{
    public class FavorisierterDrinkTable : TableBase
    {
        public string Name { get; set; } = null!;
        public virtual ProfilTable ProfilTable { get; set; }
        public int ProfilTableId { get; set; }
    }
}