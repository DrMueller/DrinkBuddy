namespace DrinkBuddy.Domain.Areas.Profile.Models
{
    public record ProfilId(int Value)
    {
        public static ProfilId Create(int value)
        {
            return new ProfilId(value);
        }
    }

    public class Profil(
        ProfilId id,
        string name,
        string beschreibung,
        IReadOnlyCollection<FavorisierterDrink> favorisierteDrinks)
        : IEquatable<Profil>
    {
        private IList<FavorisierterDrink> _favorisierteDrinks = favorisierteDrinks.ToList();

        public string Beschreibung { get; set; } = beschreibung;

        public string FavoriiserteDrinksBeschreibung => string.Join(", ", _favorisierteDrinks.Select(d => d.Name));
        public IReadOnlyCollection<FavorisierterDrink> FavorisierteDrinks => _favorisierteDrinks.AsReadOnly();
        public ProfilId Id { get; } = id;
        public string Name { get; set; } = name;

        public static Profil CreateEmpty()
        {
            return new Profil(ProfilId.Create(0), string.Empty, string.Empty, []);
        }

        public void AlignFavoriten(IReadOnlyCollection<FavorisierterDrink> drinks)
        {
            _favorisierteDrinks = drinks.ToList();
        }

        public bool Equals(Profil? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Id.Equals(other.Id);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Profil)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}