using ChatbotAI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatbotAI.Core.Repository
{
    public abstract class RepositoryCore<TDataBaseContext, TDataBaseEntity> : IRepositoryCore<TDataBaseContext, TDataBaseEntity>
        where TDataBaseContext : DbContext
        where TDataBaseEntity : class, IIdentifiable
    {
        protected TDataBaseContext DatabaseContext { get; }

        protected DbSet<TDataBaseEntity> EntitiesDatabaseSet { get; }


        public virtual async Task<TDataBaseEntity?> GetByIdAsync(Guid id)
        {
            return await EntitiesDatabaseSet.FirstOrDefaultAsync(entity => entity.Id == id).ConfigureAwait(false);
        }

        public virtual async Task AddAsync(TDataBaseEntity entity)
        {
            if (!EntitiesDatabaseSet.Any(x => x.Id == entity.Id))
            {
                await EntitiesDatabaseSet.AddAsync(entity).ConfigureAwait(false);
                await SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await DatabaseContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public RepositoryCore(TDataBaseContext databaseContext)
        {
            EntitiesDatabaseSet = databaseContext.Set<TDataBaseEntity>();
            DatabaseContext = databaseContext;
        }
    }
}
