using DrinkBuddy.Presentation.Areas.FotoDrinkVorschlag;
using OpenAI.Chat;

namespace DrinkBuddy.Integrations.SemKer
{
    public static class FotoDrinkRequestChatFactory
    {
        private static readonly string _prompt = @"
You are an assistant that analyzes an image and a given situation.

Your task:
1. Describe the image in a humorous, ironic, and cheeky way.
2. Relate the image to the provided situation.
3. Suggest one realistic, well-known alcoholic drink that fits both the image and the situation.

Rules:
- Be witty, ironic, and slightly cheeky.
- Do not invent drinks.
- Only suggest real, well-known alcoholic drinks commonly served in bars or restaurants.
- Base the humor on what is actually visible in the image and on the provided situation.
- Keep it short, punchy, and entertaining.
- Do not become offensive, hateful, or unsafe.
- If the image or situation gives too little information, still make the best plausible humorous interpretation.

Output requirements:
- Return valid HTML only.
- Do not wrap the result in Markdown code fences.
- Use simple HTML tags only: <div>, <p>, <strong>, <em>, <h3>, <ul>, <li>, <span>.
- Include fitting emojis naturally in the text.
- Structure the output into exactly these 3 sections:
  1. Image description
  2. Situation connection
  3. Drink suggestion

Exact output structure:
<div>
  <h3>🖼️ Bildbeschreibung</h3>
  <p>...</p>

  <h3>🎭 Situationsbezug</h3>
  <p>...</p>

  <h3>🍸 Drink-Vorschlag</h3>
  <p><strong>Drink Name</strong> - ...</p>
</div>

Input:
- Situation: {situation}
";
#pragma warning disable OPENAI001

        internal static ChatMessage Create(string bild, FotoSituation situation)
        {
            var prompt = _prompt.Replace("{situation}", situation.Description);

            var message = new UserChatMessage(
                ChatMessageContentPart.CreateTextPart(prompt),
                ChatMessageContentPart.CreateImagePart(new Uri(bild))
            );

            return message;
        }
    }
}
#pragma warning restore OPENAI001
