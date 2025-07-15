using ChatbotAI.Core.Interfaces;
using ChatBotAI.Infrastructure.DatabaseEntities;
using Microsoft.EntityFrameworkCore;

namespace ChatBotAI.Infrastructure.DatabaseContext;

public interface IChatbotAiContext : IDBContextCore
{
    public DbSet<AiEngineResponse> Aiengineresponses { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserRequest> Userrequests { get; set; }
}