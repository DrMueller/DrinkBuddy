using DrinkBuddy.Domain.Areas.DrinkVorschlag.Models;
using OpenAI.Chat;

namespace DrinkBuddy.Integrations.SemKer
{
    internal static class DrinkRequestChatFactory
    {
        internal static ChatMessage Create(DrinkRequest request)
        {
            var chat = new SystemChatMessage(
                ChatMessageContentPart.CreateTextPart("Antworte lustig und kreativ auf die Anfrage nach einem Drink-Vorschlag."),
                ChatMessageContentPart.CreateTextPart("Erfasse alle Texte in lustiger Form, mach Witze und sanfte Beleidungen."),
                ChatMessageContentPart.CreateTextPart("Mach eine kurze Herleitung, wie du zum Resultat gekommen bist."),
                ChatMessageContentPart.CreateTextPart("Berücksichtige die mitgegebene Situation."),
                ChatMessageContentPart.CreateTextPart("Priorisiere die Spezialwünsche höher als die Drinks aus dem Profil."),
                ChatMessageContentPart.CreateTextPart("Output: HTML mit Absätzen, Smilies etc."),
                ChatMessageContentPart.CreateTextPart($"Profil-Name: {request.Profil.Name}"),
                ChatMessageContentPart.CreateTextPart($"Profil-Beschreibung: {request.Profil.Beschreibung}"),
                ChatMessageContentPart.CreateTextPart($"Situation: {request.Situation}"),
                ChatMessageContentPart.CreateTextPart($"Profil-Drinkfavoriten: {request.Profil.FavoriiserteDrinksBeschreibung}"),
                ChatMessageContentPart.CreateTextPart($"Spezialwünsche: {request.SpezialWuensche}")
            );

            return chat;
        }
    }
}