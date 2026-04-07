using DrinkBuddy.Domain.Areas.DrinkVorschlag.Models;
using DrinkBuddy.Presentation.Areas.FotoDrinkVorschlag;

namespace DrinkBuddy.Domain.Integrations.AzureOpenAi
{
    public interface IAzureOpenAiClient
    {
        Task<string> SendDrinkRequestAsync(DrinkRequest request);
        Task<string> SendFotoDrinkRequestAsync(byte[] bild, FotoSituation fotoSituation);
    }
}
