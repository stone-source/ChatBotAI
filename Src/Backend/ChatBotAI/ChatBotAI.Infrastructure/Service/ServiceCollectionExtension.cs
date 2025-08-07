using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChatBotAI.Infrastructure.Service
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, string? connectionString)
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                services.AddDbContext<DatabaseContext.ChatbotAiContext>(options => options.UseSqlServer(connectionString));
            }

            return services;
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<Abstract.IAiEngineResponseRepository, Repository.AiEngineResponseRepository>();
            services.AddTransient<Abstract.IUserRequestRepository, Repository.UserRequestRepository>();

            return services;
        }
    }
}
