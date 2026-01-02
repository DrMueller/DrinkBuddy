using System.ComponentModel.DataAnnotations;

namespace DrinkBuddy.Presentation.Areas.Profile.Components
{
    public class ProfilEditViewModel
    {
        [Required]
        public string? Beschreibung { get; set; }

        [Required]
        public IList<string> FavorisierteDrinks { get; set; } = new List<string>();

        public int Id { get; init; }

        [Required]
        public string? Name { get; set; }
    }
}