namespace DrinkBuddy.Presentation.Areas.Drinkrad
{
    public partial class DrinkradPage
    {
        public const string Path = "/drinkrad";

        private IReadOnlyCollection<Rad.WheelSegment> StandardShots { get; } =
        [
            new("Whiskey", "#22c55e"),
            new("Jägermeister", "#3b82f6"),
            new("Tequilla", "#ef4444"),
            new("Beliner Luft", "#f97316"),
            new("Sambuca", "#a855f7"),
            new("Freie Wahl", "#facc15")
        ];
    }
}