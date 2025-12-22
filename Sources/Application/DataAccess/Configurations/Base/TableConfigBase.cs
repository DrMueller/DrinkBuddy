using DrinkBuddy.Domain.Shared.Data.Tables.Base;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrinkBuddy.DataAccess.Configurations.Base
{
    [PublicAPI("EF Core")]
    public abstract class TableConfigBase<T> : IEntityTypeConfiguration<T> where T : TableBase
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();

            builder.ToTable(typeof(T).Name);

            ConfigureEntity(builder);
        }

        protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
    }
}