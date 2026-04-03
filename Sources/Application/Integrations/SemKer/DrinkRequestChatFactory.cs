using DrinkBuddy.Domain.Areas.DrinkVorschlag.Models;
using Microsoft.SemanticKernel.ChatCompletion;

namespace DrinkBuddy.Integrations.SemKer
{
    internal static class DrinkRequestChatFactory
    {
        internal static ChatHistory Create(DrinkRequest request)
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