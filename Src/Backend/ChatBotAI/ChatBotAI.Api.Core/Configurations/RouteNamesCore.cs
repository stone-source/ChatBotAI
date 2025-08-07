using System.Diagnostics.CodeAnalysis;

namespace ChatBotAI.Api.Core.Configurations
{
    [ExcludeFromCodeCoverage]
    public abstract class RouteNamesCore
    {
        protected const string ApiRoot = "api/";

        protected const string GetById = "/get/{id}";
        protected const string Update = "/update";
        protected const string Send = "/send";
        protected const string Save = "/save";
    }
}
