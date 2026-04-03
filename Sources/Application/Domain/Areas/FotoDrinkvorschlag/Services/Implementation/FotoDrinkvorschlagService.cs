using DrinkBuddy.Domain.Integrations.SemKer;
using DrinkBuddy.Presentation.Areas.FotoDrinkVorschlag;

namespace DrinkBuddy.Domain.Areas.FotoDrinkvorschlag.Services.Implementation
{
    public class FotoDrinkvorschlagService(ISemKerClient semKerClient) : IFotoDrinkvorschlagService
    {
        public async Task<string> CreateVorschlagAsync(FotoSituation situation, string bild)
        {
            return await semKerClient.SendFotoDrinkRequestAsync(bild, situation);
        }
    }
}