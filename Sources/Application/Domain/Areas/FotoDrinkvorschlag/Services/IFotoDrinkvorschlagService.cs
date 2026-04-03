using DrinkBuddy.Presentation.Areas.FotoDrinkVorschlag;

namespace DrinkBuddy.Domain.Areas.FotoDrinkvorschlag.Services
{
    public interface IFotoDrinkvorschlagService
    {
        Task<string> CreateVorschlagAsync(FotoSituation situation, string bild);
    }
}