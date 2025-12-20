namespace DrinkBuddy.Domain.Profile.Models
{
    public class Profil
    {
        private IList<FavorisierterDrink> _favorisierteDrinks = new List<FavorisierterDrink>();

        public Profil(
            int id,
            string name,
            string beschreibung,
            IReadOnlyCollection<FavorisierterDrink> favorisierteDrinks)
        {
            ID = id;
            Name = name;
            Beschreibung = beschreibung;
            _favorisierteDrinks = favorisierteDrinks.ToList();
        }

        public void AddFavorit(FavorisierterDrink drink)
        {
            _favorisierteDrinks.Add(drink);
        }

        public void RemoveFavorit(FavorisierterDrink drink)
        {
            _favorisierteDrinks.Remove(drink);
        }

        public string Beschreibung { get; }
        public IReadOnlyCollection<FavorisierterDrink> FavorisierteDrinks => _favorisierteDrinks.AsReadOnly();
        public int ID { get; }
        public string Name { get; }
    }
}