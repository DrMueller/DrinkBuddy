using System.ComponentModel.DataAnnotations;

namespace DrinkBuddy.Presentation.Areas.Profile.Components
{
    public class ProfilEditViewModel
    {
        [Required]
        public string? Beschreibung { get; set; }

        [Required]
        [MinLength(1)]
        public IList<string> FavorisierteDrinks { get; set; } = new List<string>();

        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}