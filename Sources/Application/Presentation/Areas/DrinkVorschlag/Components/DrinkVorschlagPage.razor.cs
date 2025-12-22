using DrinkBuddy.Domain.Areas.DrinkVorschlag.Services;
using DrinkBuddy.Domain.Areas.Profile.Models;
using DrinkBuddy.Domain.Areas.Profile.Specifications;
using DrinkBuddy.Domain.Shared.Data.Querying;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DrinkBuddy.Presentation.Areas.DrinkVorschlag.Components
{
    public partial class DrinkVorschlagPage
    {
        public const string Path = "/drinkvorschlag";

        [Inject]
        public required IDialogService DialogService { get; set; }

        [Inject]
        public required IDrinkVorschlagService DrinkVorschlagService { get; set; }

        [Inject]
        public required IQueryService QueryService { get; set; }

        private DrinkVorschlagViewModel EditModel { get; } = new();

        private bool IsLoading => Profile == null;

        private IReadOnlyCollection<Profil>? Profile { get; set; }
        public bool IsCalculating { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Profile = await QueryService.QueryAsync(new ProfilSpec());
        }

        private async Task CreateVorschlagAsync()
        {
            IsCalculating = true;

            var drink = await DrinkVorschlagService.CreateVorschlagAsync(
                ProfilId.Create(EditModel.SelectedProfil!.Id.Value),
                EditModel.Situation ?? string.Empty,
                EditModel.SpezialWuensche ?? string.Empty);

            var options = new DialogOptions
            {
                FullWidth = true,
                MaxWidth = MaxWidth.Medium
            };

            IsCalculating = false;

            var parameters = new DialogParameters<DrinkVorschlagDialog> { { x => x.Text, drink } };
            await DialogService.ShowAsync<DrinkVorschlagDialog>("Drinks!", parameters, options);
        }
    }
}