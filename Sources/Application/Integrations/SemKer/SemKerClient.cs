using System.Text;
using DrinkBuddy.Common.Settings.Provisioning.Services;
using DrinkBuddy.Domain.Areas.DrinkVorschlag.Models;
using DrinkBuddy.Domain.Integrations.SemKer;
using JetBrains.Annotations;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace DrinkBuddy.Integrations.SemKer
{
    [UsedImplicitly]
    public class SemKerClient(ISettingsProvider settingsProvider) : ISemKerClient
    {
        public async Task<string> SendAsync(DrinkRequest request)
        {
            var chat = CreateChat(request);

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

        private static ChatHistory CreateChat(DrinkRequest request)
        {
            var chat = new ChatHistory();

            chat.AddSystemMessage("Antworte lustig und kreativ auf die Anfrage nach einem Drink-Vorschlag.");
            chat.AddSystemMessage("Erfasse alle Texte in lustiger Form, mach Witze und sanfte Beleidungen");
            chat.AddSystemMessage("Mach eine kurze Herleitung, wie du zum Resultat gekommen bist.");
            chat.AddSystemMessage("Berücksichtige die mitgegebene Situation.");
            chat.AddSystemMessage("Priorisiere die Spezialwünsche höher als die Drinks aus dem Profil.");
            chat.AddSystemMessage("Output: HTML mit Absätzen, Smilies etc.");

            chat.AddUserMessage($"Profil-Name: {request.Profil.Name}");
            chat.AddUserMessage($"Profil-Beschreibung: {request.Profil.Beschreibung}");
            chat.AddUserMessage($"Profil-Drinkfavoriten: {request.Profil.FavoriiserteDrinksBeschreibung}");
            chat.AddUserMessage($"Situation: {request.Situation}");
            chat.AddUserMessage($"Spezialwünsche: {request.SpezialWuensche}");

            return chat;
        }
    }
}