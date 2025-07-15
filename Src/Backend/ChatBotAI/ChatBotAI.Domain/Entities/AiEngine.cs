using ChatBotAI.Domain.Abstract;

namespace ChatBotAI.Domain.Entities
{
    public class AiEngine : AiEngineCore
    {
        public AiEngine(IAiEngineStrategyCore engineStrategy)
        {
            EngineStrategy = engineStrategy;
        }
    }
}
