using DrinkBuddy.Common.LanguageExtensions.Invariance;

namespace DrinkBuddy.Common.InformationHandling
{
    public class InformationEntry
    {
        public InformationEntry(InformationType type, string message)
        {
            Guard.StringNotNullOrEmpty(() => message);

            Type = type;
            Message = message;
        }

        public string Message { get; }
        public InformationType Type { get; }
    }
}