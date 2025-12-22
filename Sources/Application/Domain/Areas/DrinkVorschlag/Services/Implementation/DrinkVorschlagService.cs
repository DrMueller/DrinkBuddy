using DrinkBuddy.Domain.Areas.DrinkVorschlag.Models;
using DrinkBuddy.Domain.Areas.Profile.Models;
using DrinkBuddy.Domain.Areas.Profile.Specifications;
using DrinkBuddy.Domain.Integrations.SemKer;
using DrinkBuddy.Domain.Shared.Data.Querying;

namespace DrinkBuddy.Domain.Areas.DrinkVorschlag.Services.Implementation
{
    public class DrinkVorschlagService(
        IQueryService queryService,
        ISemKerClient semKerClient)
        : IDrinkVorschlagService
    {
        public async Task<string> CreateVorschlagAsync(ProfilId profilId, string situation, string spezialWuensche)
        {
            var profil = await queryService.QuerySingleAsync(new ProfilSpec(profilId));
            var request = new DrinkRequest(profil, situation, spezialWuensche);

            return await semKerClient.SendAsync(request);
        }
    }
}