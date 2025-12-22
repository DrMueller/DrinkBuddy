using DrinkBuddy.Domain.Areas.Profile.Models;
using DrinkBuddy.Domain.Shared.Data.Writing;

namespace DrinkBuddy.Domain.Areas.Profile.Repositories
{
    public interface IProfilRepository : IRepository
    {
        Task DeleteAsync(ProfilId profileId);
        Task SaveAsync(Profil profil);
    }
}
