using System.ComponentModel.DataAnnotations;
using DrinkBuddy.Domain.Areas.Profile.Models;

namespace DrinkBuddy.Presentation.Areas.DrinkVorschlag.Components
{
    public class DrinkVorschlagViewModel
    {
        [Required]
        public Profil? SelectedProfil { get; set; }

        public string? Situation { get; set; }

        public string? SpezialWuensche { get; set; }
    }
}
