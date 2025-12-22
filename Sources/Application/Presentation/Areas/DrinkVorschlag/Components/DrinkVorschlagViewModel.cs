using DrinkBuddy.Domain.Areas.Profile.Models;

namespace DrinkBuddy.Presentation.Areas.DrinkVorschlag.Components
{
    public class DrinkVorschlagViewModel
    {
        public Profil? SelectedProfil { get; set; }

        public string? Situation { get; set; }

        public string? SpezialWuensche { get; set; }
    }
}
