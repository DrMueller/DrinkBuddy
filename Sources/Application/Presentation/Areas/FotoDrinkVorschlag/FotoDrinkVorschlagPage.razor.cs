using DrinkBuddy.Domain.Areas.FotoDrinkvorschlag.Services;
using DrinkBuddy.Presentation.Areas.DrinkVorschlag.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DrinkBuddy.Presentation.Areas.FotoDrinkVorschlag
{
    public partial class FotoDrinkVorschlagPage
    {
        public const string Path = "/fotodrinkvorschlag";

        private string? _bild;

        [Inject]
        public required IDialogService DialogService { get; set; }

        public bool IsCalculating { get; set; }

        [Inject]
        public required IFotoDrinkvorschlagService Service { get; set; }

        private bool IsDisabled => string.IsNullOrEmpty(_bild);

        private FotoSituation SelectedSituation { get; set; } = FotoSituation.CreateAll().First();

        private Task HandlePictureTakenAsync(string arg)
        {
            _bild = arg;
            return Task.CompletedTask;
        }

        private async Task SuggestDrinkAsync()
        {
            if (string.IsNullOrEmpty(_bild))
            {
                return;
            }

            IsCalculating = true;

            var vorschlag = await Service.CreateVorschlagAsync(SelectedSituation, _bild);
            IsCalculating = false;

            var options = new DialogOptions
            {
                FullWidth = true,
                MaxWidth = MaxWidth.Medium
            };

            IsCalculating = false;

            var parameters = new DialogParameters<DrinkVorschlagDialog> { { x => x.Text, vorschlag } };
            await DialogService.ShowAsync<DrinkVorschlagDialog>("Drinks!", parameters, options);
        }
    }
}