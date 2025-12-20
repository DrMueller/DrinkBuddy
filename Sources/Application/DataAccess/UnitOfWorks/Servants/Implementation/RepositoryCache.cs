using System.Collections.Concurrent;
using DrinkBuddy.Common.LanguageExtensions.Types.Maybes;
using DrinkBuddy.DataAccess.DbContexts.Contexts;
using DrinkBuddy.DataAccess.Repositories.Base;
using DrinkBuddy.Domain.Shared.Data.Writing;
using JetBrains.Annotations;
using Lamar;

namespace DrinkBuddy.DataAccess.UnitOfWorks.Servants.Implementation
{
    [UsedImplicitly]
    public class RepositoryCache(IContainer container) : IRepositoryCache
    {
        private readonly ConcurrentDictionary<Type, IRepository> _repos = new();

        public TRepo GetRepository<TRepo>(IAppDbContext dbContext)
            where TRepo : IRepository
        {
            var getRepoResult = TryGettingRepository<TRepo>();
            var repo = getRepoResult.Reduce(() => InitializeRepository<TRepo>(dbContext));

            return repo;
        }

        private TRepo InitializeRepository<TRepo>(IAppDbContext dbContext)
            where TRepo : IRepository
        {
            var repository = container.GetInstance<TRepo>();

            if (!(repository is IRepositoryBase repoBase))
            {
                throw new ArgumentException($"{nameof(TRepo)} does not implement RepositoryBase");
            }

            repoBase.Initialize(dbContext);
            _repos.AddOrUpdate(repository.GetType(), repository, (_, repo) => repo);

            return repository;
        }

        private Maybe<TRepo> TryGettingRepository<TRepo>()
            where TRepo : IRepository
        {
            var repoType = typeof(TRepo);

            // For some reason, TryGetValue doesn't work here
            var cachedRepo = _repos.SingleOrDefault(f => repoType.IsAssignableFrom(f.Key));
            var castedRepo = (TRepo)cachedRepo.Value;

            return MaybeFactory.CreateFromNullable(castedRepo);
        }
    }
}