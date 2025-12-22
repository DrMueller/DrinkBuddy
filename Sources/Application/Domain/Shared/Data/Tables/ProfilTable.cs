using DrinkBuddy.Domain.Shared.Data.Tables.Base;
using JetBrains.Annotations;

namespace DrinkBuddy.Domain.Shared.Data.Tables
{
    [PublicAPI("EF Core")]
    public class ProfilTable : TableBase
    {
        public string Beschreibung { get; set; } = null!;
        public ICollection<FavorisierterDrinkTable> FavorisierteDrinks { get; set; } = new List<FavorisierterDrinkTable>();
        public string Name { get; set; } = null!;
    }
}