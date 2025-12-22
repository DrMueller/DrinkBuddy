using DrinkBuddy.DataAccess.Configurations.Base;
using DrinkBuddy.Domain.Shared.Data.Tables;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrinkBuddy.DataAccess.Configurations
{
    public class ProfilTableConfig : TableConfigBase<ProfilTable>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<ProfilTable> builder)
        {
            builder.Property(f => f.Name).HasMaxLength(20);
            builder.Property(f => f.Beschreibung).HasMaxLength(255);
        }
    }
}
