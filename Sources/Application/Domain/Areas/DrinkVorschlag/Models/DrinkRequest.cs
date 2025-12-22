using DrinkBuddy.Domain.Areas.Profile.Models;

namespace DrinkBuddy.Domain.Areas.DrinkVorschlag.Models
{
    public class DrinkRequest(
        Profil profil,
        string situation,
        string spezialWünsche)
    {
        public Profil Profil { get; } = profil;
        public string Situation { get; } = situation;
        public string SpezialWünsche { get; } = spezialWünsche;
    }
}