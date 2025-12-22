using DrinkBuddy.Common.LanguageExtensions.Types.Maybes;
using DrinkBuddy.Common.LanguageExtensions.Types.Maybes.Implementation;
using DrinkBuddy.DataAccess;
using DrinkBuddy.Domain.Areas.Profile.Models;
using DrinkBuddy.Domain.Shared.Data.Querying;
using DrinkBuddy.Domain.Shared.Data.Tables;
using Microsoft.EntityFrameworkCore;

namespace DrinkBuddy.Domain.Areas.Profile.Specifications
{
    public class ProfilSpec : IQuerySpecification<Profil>
    {
        private readonly Maybe<ProfilId> _profilId = None.Value;

        public ProfilSpec()
        {
        }

        public ProfilSpec(ProfilId profilId)
        {
            _profilId = profilId;
        }

        public IQueryable<Profil> Apply(IQueryBase qryProvider)
        {
            var qry = qryProvider.Query<ProfilTable>()
                .Include(f => f.FavorisierteDrinks)
                .Select(f => f);

            qry = qry.WhereOptional(_profilId, profilId => q => q.Id == profilId.Value);

            var modelQry = qry.Select(f => new Profil(
                ProfilId.Create(f.Id),
                f.Name,
                f.Beschreibung,
                f.FavorisierteDrinks
                    .Select(fd => new FavorisierterDrink(fd.Name))
                    .ToList()));

            return modelQry;
        }
    }
}