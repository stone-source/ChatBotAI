using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace ChatBotAI.Domain.Abstract
{
    [ExcludeFromCodeCoverage]
    public abstract class AiStrategyCore : IAiEngineStrategyCore
    {
        protected readonly IConfiguration _configuration;

        protected readonly CancellationToken _cancellationToken;

        protected abstract string ConfigurationApiKeyPath { get; }

        protected string GetApiKey()
        {
            return _configuration[ConfigurationApiKeyPath];
        }

        public abstract void AddClient(string clientApiKey);

        public abstract void AddSystemChatMessage();

        public abstract void AddUserChatMessage(string userRequest);

        public abstract IAsyncEnumerable<string> CallClientAndReturnResponse();

        protected abstract string GetSystemPrompt();

        public AiStrategyCore(IConfiguration configuration, CancellationToken cancellationToken)
        {
            _configuration = configuration;
            _cancellationToken = cancellationToken;
        }
    }
}
