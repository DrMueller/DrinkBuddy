using Microsoft.AspNetCore.Components;

namespace DrinkBuddy.Presentation.Shared.Infos
{
    public partial class InfoList
    {
        [Parameter]
        public IReadOnlyCollection<string> InfoEntries { get; set; } = null!;
    }
}