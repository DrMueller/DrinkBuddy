using DrinkBuddy.DataAccess.Querying;
using DrinkBuddy.DataAccess.UnitOfWorks.Implementation;
using DrinkBuddy.Domain.Shared.Data.Querying;
using DrinkBuddy.Domain.Shared.Data.Writing;
using JetBrains.Annotations;
using Lamar;

namespace DrinkBuddy
{
    [UsedImplicitly]
    public class ServiceRegistryCollection : ServiceRegistry
    {
        public ServiceRegistryCollection()
        {
            Scan(scanner =>
            {
                scanner.AssemblyContainingType<ServiceRegistryCollection>();
                scanner.AddAllTypesOf<IRepository>();
                scanner.WithDefaultConventions();
            });

            For<IQueryService>().Use<QueryService>().Singleton();
            For<IUnitOfWorkFactory>().Use<UnitOfWorkFactory>().Singleton();
        }
    }
}