using DrinkBuddy.Domain.Areas.Profile.Models;

namespace DrinkBuddy.Domain.Areas.DrinkVorschlag.Models
{
    public class DrinkRequest(
        Profil profil,
        string situation,
        string spezialWuensche)
    {
        public Profil Profil { get; } = profil;
        public string Situation { get; } = string.IsNullOrEmpty(situation) ? "Keine" : situation;
        public string SpezialWuensche { get; } = string.IsNullOrEmpty(spezialWuensche) ? "Keine" : spezialWuensche;
    }
}