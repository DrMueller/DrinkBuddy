using DrinkBuddy.Domain.Areas.FotoDrinkvorschlag.Services;
using DrinkBuddy.Presentation.Areas.DrinkVorschlag.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DrinkBuddy.Presentation.Areas.FotoDrinkVorschlag
{
    public partial class FotoDrinkVorschlagPage
    {
        public const string Path = "/fotodrinkvorschlag";

        private byte[]? _bild;

        [Inject]
        public required IDialogService DialogService { get; set; }

        public bool IsCalculating { get; set; }

        [Inject]
        public required IFotoDrinkvorschlagService Service { get; set; }

        private bool IsDisabled => _bild == null || IsCalculating;

        private FotoSituation SelectedSituation { get; set; } = FotoSituation.CreateAll().First();

        private Task HandlePictureTakenAsync(byte[] arg)
        {
            _bild = arg;
            StateHasChanged();
            return Task.CompletedTask;
        }

        private async Task SuggestDrinkAsync()
        {
            if (_bild == null)
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