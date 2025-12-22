using DrinkBuddy.Common.InformationHandling;
using DrinkBuddy.Domain.Areas.Profile.Models;
using DrinkBuddy.Presentation.Areas.Profile.Components;

namespace DrinkBuddy.Presentation.Areas.Profile.Services
{
    public interface IProfilService
    {
        Task DeleteAsync(ProfilId profilId);
        Task<ProfilEditViewModel> LoadEditAsync(ProfilId profilId);
        Task<InformationEntries> SaveAsync(ProfilEditViewModel editModel);
    }
}
