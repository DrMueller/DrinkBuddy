using DrinkBuddy.Common.Extensions;
using DrinkBuddy.Common.InformationHandling;
using DrinkBuddy.Domain.Areas.Profile.Models;
using DrinkBuddy.Domain.Areas.Profile.Repositories;
using DrinkBuddy.Domain.Areas.Profile.Specifications;
using DrinkBuddy.Domain.Shared.Data.Querying;
using DrinkBuddy.Domain.Shared.Data.Writing;
using DrinkBuddy.Presentation.Areas.Profile.Components;

namespace DrinkBuddy.Presentation.Areas.Profile.Services.Implementation
{
    public class ProfilService(
        IUnitOfWorkFactory uowFactory,
        IQueryService queryService)
        : IProfilService
    {
        public async Task DeleteAsync(ProfilId profilId)
        {
            using var uow = uowFactory.Create();
            var profilRepo = uow.GetRepository<IProfilRepository>();
            await profilRepo.DeleteAsync(profilId);
            await uow.CommitAsync();
        }

        public async Task<ProfilEditViewModel> LoadEditAsync(ProfilId profilId)
        {
            if (profilId.Value == 0)
            {
                return new ProfilEditViewModel
                {
                    Id = 0
                };
            }

            return await queryService
                .QuerySingleAsync(new ProfilSpec(profilId))
                .MapAsync(s => new ProfilEditViewModel
                {
                    Id = s.Id.Value,
                    Beschreibung = s.Beschreibung,
                    Name = s.Name,
                    FavorisierteDrinks = s.FavorisierteDrinks
                        .Select(fd => fd.Name)
                        .ToList()
                });
        }

        public async Task<InformationEntries> SaveAsync(ProfilEditViewModel editModel)
        {
            var profil = await LoadProfilAsync(ProfilId.Create(editModel.Id));
            var favoriten = editModel.FavorisierteDrinks
                .Select(fd => new FavorisierterDrink(fd))
                .ToList();

            profil.Name = editModel.Name ?? string.Empty;
            profil.Beschreibung = editModel.Beschreibung ?? string.Empty;
            profil.AlignFavoriten(favoriten);

            using var uow = uowFactory.Create();
            var profilRepo = uow.GetRepository<IProfilRepository>();
            await profilRepo.SaveAsync(profil);

            await uow.CommitAsync();

            return InformationEntries.Empty;
        }

        private async Task<Profil> LoadProfilAsync(ProfilId profilId)
        {
            if (profilId.Value > 0)
            {
                return await queryService
                    .QuerySingleAsync(new ProfilSpec(profilId));
            }

            return Profil.CreateEmpty();
        }
    }
}