using DrinkBuddy.Domain.Shared.Data.Tables.Base;

namespace DrinkBuddy.Domain.Shared.Data.Tables
{
    public class ProfilTable : TableBase
    {
        public string Beschreibung { get; set; } = null!;
        public ICollection<FavorisierterDrinkTable> FavorisierteDrinks { get; set; } = new List<FavorisierterDrinkTable>();
        public string Name { get; set; } = null!;
    }
}