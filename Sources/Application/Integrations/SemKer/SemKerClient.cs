using System.Text;
using Azure;
using Azure.AI.OpenAI;
using DrinkBuddy.Common.Settings.Provisioning.Services;
using DrinkBuddy.Domain.Areas.DrinkVorschlag.Models;
using DrinkBuddy.Domain.Integrations.SemKer;
using DrinkBuddy.Presentation.Areas.FotoDrinkVorschlag;
using JetBrains.Annotations;
using OpenAI.Chat;

namespace DrinkBuddy.Integrations.SemKer
{
    [UsedImplicitly]
    public class SemKerClient(ISettingsProvider settingsProvider) : ISemKerClient
    {
        public async Task<string> SendDrinkRequestAsync(DrinkRequest request)
        {
            var message = DrinkRequestChatFactory.Create(request);
            return await SendAsync(message);
        }

        public async Task<string> SendFotoDrinkRequestAsync(string bild, FotoSituation situation)
        {
            var message = FotoDrinkRequestChatFactory.Create(bild, situation);
            return await SendAsync(message);
        }

        private async Task<string> SendAsync(ChatMessage message)
        {
            var client = new AzureOpenAIClient(
                new Uri(settingsProvider.AppSettings.OpenAiEndpoint),
                new AzureKeyCredential(settingsProvider.AppSettings.OpenAiKey)
            );

            var chatClient = client.GetChatClient(settingsProvider.AppSettings.OpenAiDeploymentName);
            var response = await chatClient.CompleteChatAsync(message);
            var result = response.Value.Content.Aggregate(new StringBuilder(), (sb, msg) => sb.AppendLine(msg.Text)).ToString();
            result = result.Replace("```", string.Empty);

            return result;
        }
    }
}