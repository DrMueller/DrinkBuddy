using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DrinkBuddy.Presentation.Shared.LoadingIndication
{
    public partial class LoadingButton
    {
        [Parameter]
        [EditorRequired]
        public required ButtonType ButtonType { get; set; }

        [Parameter]
        [EditorRequired]
        public required string Class { get; set; }

        [Parameter]
        [EditorRequired]
        public required string Icon { get; set; }

        [Parameter]
        [EditorRequired]
        public bool IsLoading { get; set; }

        [Parameter]
        public required EventCallback OnClick { get; set; }
    }
}