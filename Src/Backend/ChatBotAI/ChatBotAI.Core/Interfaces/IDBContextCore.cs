using Microsoft.EntityFrameworkCore;

namespace ChatbotAI.Core.Interfaces;

public interface IDBContextCore
{
    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;
}