using DrinkBuddy.Presentation.Infrastructure.Navigation.Models;
using DrinkBuddy.Presentation.Infrastructure.Navigation.Services;
using Microsoft.AspNetCore.Components;

namespace DrinkBuddy.Presentation.Areas.Profile.Components
{
    public partial class ProfileOverviewPage
    {
        public const string Path = "/profile/overview";

        [Inject]
        public required INavigator Navigator { get; set; }

        private void CreateNewProfil()
        {
            Navigator.NavigateTo(AppPath.Create(ProfilEditPage.Path).Format(0), true);
        }
    }
}
