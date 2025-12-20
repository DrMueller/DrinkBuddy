namespace DrinkBuddy.Domain.Shared.Data.Writing
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}