using Microsoft.EntityFrameworkCore;

namespace ChatBotAI.Infrastructure.Core.Interfaces;

public interface IDBContextCore
{
    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;
}
