namespace ChatBotAI.Domain.Abstract
{
    public abstract class AiEngineCore
    {
        private string ApiKey { get; set; }

        protected IAiEngineStrategyCore EngineStrategy { get; set; }
        
        public virtual void BuildAiEngine()
        {
            EngineStrategy.AddClient(ApiKey);
            EngineStrategy.AddSystemChatMessage();
        }

        public virtual void AddUserRequestMessage(string userRequestMessage)
        {
            EngineStrategy.AddUserChatMessage(userRequestMessage);
        }

        public virtual IAsyncEnumerable<string> CallClientAndReturnResponse()
        {
            return EngineStrategy.CallClientAndReturnResponse();
        }
    }
}
