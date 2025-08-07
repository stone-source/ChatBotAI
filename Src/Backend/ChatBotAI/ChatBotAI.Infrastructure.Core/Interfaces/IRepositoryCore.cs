namespace ChatBotAI.Infrastructure.Core.Interfaces
{
    public interface IRepositoryCore<TContext, TEntity>
        where TContext : class
        where TEntity : class, IIdentifiable
    {
        Task<TEntity> GetByIdAsync(Guid id);

        Task AddAsync(TEntity entity);

        Task SaveChangesAsync();
    }
}
