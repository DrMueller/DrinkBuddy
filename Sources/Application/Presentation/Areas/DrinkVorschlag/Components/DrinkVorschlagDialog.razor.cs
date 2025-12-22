using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DrinkBuddy.Presentation.Areas.DrinkVorschlag.Components
{
    public partial class DrinkVorschlagDialog
    {
        [CascadingParameter]
        public required IMudDialogInstance MudDialog { get; set; }

        [Parameter]
        public required string Text { get; set; }

        private void Cancel()
        {
            MudDialog.Cancel();
        }
    }
}