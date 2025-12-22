using JetBrains.Annotations;

namespace DrinkBuddy.Domain.Shared.Data.Tables.Base
{
    [PublicAPI("EF Core")]
    public class TableBase
    {
        public int Id { get; set; }
    }
}