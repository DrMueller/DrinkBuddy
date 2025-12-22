using DrinkBuddy.Domain.Areas.Profile.Models;
using DrinkBuddy.Domain.Areas.Profile.Specifications;
using DrinkBuddy.Domain.Shared.Data.Querying;
using DrinkBuddy.Presentation.Areas.Profile.Services;
using DrinkBuddy.Presentation.Infrastructure.Navigation.Models;
using DrinkBuddy.Presentation.Infrastructure.Navigation.Services;
using Microsoft.AspNetCore.Components;

namespace DrinkBuddy.Presentation.Areas.Profile.Components
{
    public partial class ProfileOverviewList
    {
        [Inject]
        public required INavigator Navigator { get; set; }

        [Inject]
        public required IProfilService ProfilService { get; set; }

        [Inject]
        public required IQueryService QueryService { get; set; }

        private bool IsLoading => Profile == null;
        private IReadOnlyCollection<Profil>? Profile { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task DeleteProfilAsync(ProfilId profilId)
        {
            await ProfilService.DeleteAsync(profilId);
            await LoadAsync();
        }

        private void EditProfil(ProfilId profilId)
        {
            Navigator.NavigateTo(AppPath.Create(ProfilEditPage.Path).Format(profilId.Value), true);
        }

        private async Task LoadAsync()
        {
            Profile = await QueryService.QueryAsync(new ProfilSpec());
        }
    }
}