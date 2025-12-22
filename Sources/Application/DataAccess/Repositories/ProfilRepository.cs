using DrinkBuddy.DataAccess.Repositories.Base;
using DrinkBuddy.Domain.Areas.Profile.Models;
using DrinkBuddy.Domain.Areas.Profile.Repositories;
using DrinkBuddy.Domain.Shared.Data.Tables;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace DrinkBuddy.DataAccess.Repositories
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ProfilRepository : RepositoryBase, IProfilRepository
    {
        public async Task DeleteAsync(ProfilId profileId)
        {
            await RemoveAsync<ProfilTable>(profileId.Value);
        }

        public async Task SaveAsync(Profil profil)
        {
            var table = await LoadSertAsync(profil.Id);
            table.Name = profil.Name;
            table.Beschreibung = profil.Beschreibung;
            table.FavorisierteDrinks.Clear();
            foreach (var favDrink in profil.FavorisierteDrinks)
            {
                table.FavorisierteDrinks.Add(new FavorisierterDrinkTable
                {
                    Name = favDrink.Name
                });
            }
        }

        private async Task<ProfilTable> LoadSertAsync(
            ProfilId id)
        {
            if (id.Value == 0)
            {
                var newProfil = new ProfilTable();
                await AddAsync(newProfil);

                return newProfil;
            }

            var qry = Query<ProfilTable>()
                .Include(f => f.FavorisierteDrinks)
                .Where(f => f.Id == id.Value);

            return await qry.SingleAsync();
        }
    }
}