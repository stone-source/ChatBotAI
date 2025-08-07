using ChatBotAI.Api.Core.Configurations;
using System.Diagnostics.CodeAnalysis;

namespace ChatBotAI.API.Configuration
{
    [ExcludeFromCodeCoverage]
    public abstract class RouteNamesV1 : RouteNamesCore
    {
        public abstract class V1ApiRoute
        {
            public const string RouteNamesV1ApiRoute = $"{ApiRoot}{ApiVersionsCore.Version1}";
        }

        public abstract class AiEngineResponseRoute
        {
            protected const string AiEngineResponseModuleRoute = $"{V1ApiRoute.RouteNamesV1ApiRoute}/ai-engine-response";

            public const string SaveAiEngineResponse = $"{AiEngineResponseModuleRoute}{Save}";
            public const string UpdateAiEngineResponseRate = $"{AiEngineResponseModuleRoute}/rate{Update}";
        }

        public abstract class ConversationRoute
        {
            protected const string ConversationModuleRoute = $"{V1ApiRoute.RouteNamesV1ApiRoute}/conversations";

            public const string GetConversationsByUserId = $"{ConversationModuleRoute}{GetById}";
        }

        public abstract class UserRequestRoute
        {
            protected const string UserRequestModuleRoute = $"{V1ApiRoute.RouteNamesV1ApiRoute}/user-requests";

            public const string UserRequestCompletions = $"{UserRequestModuleRoute}/completions{Send}";
        }
    }
}
