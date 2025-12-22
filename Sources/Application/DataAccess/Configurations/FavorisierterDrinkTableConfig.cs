using DrinkBuddy.DataAccess.Configurations.Base;
using DrinkBuddy.Domain.Shared.Data.Tables;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrinkBuddy.DataAccess.Configurations
{
    public class FavorisierterDrinkTableConfig : TableConfigBase<FavorisierterDrinkTable>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<FavorisierterDrinkTable> builder)
        {
            builder.Property(f => f.Name).HasMaxLength(100);

            builder
                .HasOne(f => f.ProfilTable)
                .WithMany(f => f.FavorisierteDrinks)
                .HasForeignKey(f => f.ProfilTableId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}