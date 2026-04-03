using DrinkBuddy.Domain.Areas.DrinkVorschlag.Models;
using DrinkBuddy.Presentation.Areas.FotoDrinkVorschlag;

namespace DrinkBuddy.Domain.Integrations.SemKer
{
    public interface ISemKerClient
    {
        Task<string> SendDrinkRequestAsync(DrinkRequest request);
        Task<string> SendFotoDrinkRequestAsync(string bild, FotoSituation fotoSituation);
    }
}
