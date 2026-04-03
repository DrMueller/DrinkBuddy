namespace DrinkBuddy.Presentation.Areas.FotoDrinkVorschlag
{
    public record FotoSituation(string Name, string Description)
    {
        public static IReadOnlyCollection<FotoSituation> CreateAll()
        {
            return new List<FotoSituation>
            {
                new("Grobe Party", "Eine ausgelassene Feier. Shots, Whisky, Gin, Everythign goes"),
                new("Gemütliche Party", "Gemütliches Fest. Bier, weniger starke Shots, Martini etc."),
                new("Feierabendbier", "Feierabendbier. Bier etc."),
                new("Apero", "Ein Aperitif. Leichte Getränke, Bier etc.")
            };
        }
    }
}