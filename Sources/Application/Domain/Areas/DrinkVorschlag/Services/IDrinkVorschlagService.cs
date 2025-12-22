using DrinkBuddy.Domain.Areas.Profile.Models;

namespace DrinkBuddy.Domain.Areas.DrinkVorschlag.Services
{
    public interface IDrinkVorschlagService
    {
        Task<string> CreateVorschlagAsync(ProfilId profilId, string situation, string spezialWuensche);
    }
}