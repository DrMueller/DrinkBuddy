using DrinkBuddy.Domain.Integrations.AzureOpenAi;
using DrinkBuddy.Presentation.Areas.FotoDrinkVorschlag;

namespace DrinkBuddy.Domain.Areas.FotoDrinkvorschlag.Services.Implementation
{
    public class FotoDrinkvorschlagService(IAzureOpenAiClient azureOpenAiClient) : IFotoDrinkvorschlagService
    {
        public async Task<string> CreateVorschlagAsync(FotoSituation situation, byte[] bild)
        {
            return await azureOpenAiClient.SendFotoDrinkRequestAsync(bild, situation);
        }
    }
}