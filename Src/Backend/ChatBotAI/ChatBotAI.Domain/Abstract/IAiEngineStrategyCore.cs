namespace ChatBotAI.Domain.Abstract
{
    public interface IAiEngineStrategyCore
    {
        void AddClient(string clientApiKey);

        void AddSystemChatMessage();

        void AddUserChatMessage(string userRequest);

        IAsyncEnumerable<string> CallClientAndReturnResponse(CancellationToken cancellationToken = default);
    }
}
