using ChatBotAI.Application.AiEngineResponses.Handlers;
using ChatBotAI.Application.Conversations.Handlers;
using ChatBotAI.Application.UserRequests.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace ChatBotAI.Application.Service
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AiEngineResponseSaveResponseHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AIEngineResponseUpdateRateHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ConversationGetAllByUserIdHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UserRequestCompletionsHandler).Assembly));

            return services;
        }
    }
}
