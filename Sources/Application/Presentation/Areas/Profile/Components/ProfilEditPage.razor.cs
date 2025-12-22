using DrinkBuddy.Common.InformationHandling;
using DrinkBuddy.Presentation.Areas.Profile.Services;
using DrinkBuddy.Presentation.Infrastructure.Navigation.Services;
using Microsoft.AspNetCore.Components;

namespace DrinkBuddy.Presentation.Areas.Profile.Components
{
    public partial class ProfilEditPage
    {
        public const string Path = "/profile/edit/{profilId:int}";

        public ProfilEditViewModel? EditModel { get; set; }

        [Inject]
        public required INavigator Navigator { get; set; }

        [Parameter]
        public int ProfilId { get; set; }

        [Inject]
        public required IProfilService ProfilService { get; set; }

        private InformationEntries? Infos { get; set; }
        private bool IsLoading => EditModel == null;

        protected override async Task OnInitializedAsync()
        {
            EditModel = await ProfilService.LoadEditAsync(Domain.Areas.Profile.Models.ProfilId.Create(ProfilId));
        }

        private void Cancel()
        {
            Navigator.NavigateTo(ProfileOverviewPage.Path);
        }

        private async Task SaveAsync()
        {
            Infos = await ProfilService.SaveAsync(EditModel!);
            if (Infos.IsEmpty)
            {
                Navigator.NavigateTo(ProfileOverviewPage.Path);
            }
        }
    }
}