using System.Text;
using DrinkBuddy.Common.Settings.Provisioning.Services;
using DrinkBuddy.Domain.Areas.DrinkVorschlag.Models;
using DrinkBuddy.Domain.Integrations.SemKer;
using DrinkBuddy.Presentation.Areas.FotoDrinkVorschlag;
using JetBrains.Annotations;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace DrinkBuddy.Integrations.SemKer
{
    [UsedImplicitly]
    public class SemKerClient(ISettingsProvider settingsProvider) : ISemKerClient
    {
        public async Task<string> SendDrinkRequestAsync(DrinkRequest request)
        {
            var chat = DrinkRequestChatFactory.Create(request);
            return await SendAsync(chat);
        }

        public Task<string> SendFotoDrinkRequestAsync(string bild, FotoSituation situation)
        {
            var chat = FotoDrinkRequestChatFactory.Create(bild, situation);
            return SendAsync(chat);
        }

        private async Task<string> SendAsync(ChatHistory chat)
        {
            var kernelBuilder = Kernel.CreateBuilder();
            kernelBuilder.AddAzureOpenAIChatCompletion(
                settingsProvider.AppSettings.OpenAiDeploymentName,
                settingsProvider.AppSettings.OpenAiEndpoint,
                settingsProvider.AppSettings.OpenAiKey,
                modelId: settingsProvider.AppSettings.OpenAiModelId);

            var kernel = kernelBuilder.Build();
            var chatService = kernel.Services.GetRequiredService<IChatCompletionService>();
            var reply = await chatService.GetChatMessageContentsAsync(chat);

            var result = reply.Aggregate(new StringBuilder(), (sb, msg) => sb.AppendLine(msg.Content)).ToString();
            result = result.Replace("```", string.Empty);

            return result;
        }
    }
}