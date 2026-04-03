namespace DrinkBuddy.Presentation.Areas.FotoDrinkVorschlag
{
    public record FotoSituation(string Value, string Name, string Description)
    {
        public static IReadOnlyCollection<FotoSituation> CreateAll()
        {
            return new List<FotoSituation>
            {
                new("PartyGrob", "Grobe Party", "Eine ausgelassene Feier. Shots, Whisky, Gin, Everythign goes"),
                new("PartyGemuetlich", "Gemütliche Party", "Gemütliches Fest. Bier, weniger starke Shots, Martini etc."),
                new("Feierabendbier", "Feierabendbier", "Feierabendbier. Bier etc."),
                new("Apero", "Apero", "Ein Aperitif. Leichte Getränke, Bier etc.")
            };
        }
    }
}