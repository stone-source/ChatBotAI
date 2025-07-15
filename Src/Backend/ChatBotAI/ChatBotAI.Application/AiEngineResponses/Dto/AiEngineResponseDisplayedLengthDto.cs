using System.Diagnostics.CodeAnalysis;
using ChatbotAI.Core.Interfaces;

namespace ChatBotAI.Application.AiEngineResponses.Dto
{
    [ExcludeFromCodeCoverage]
    public sealed class AiEngineResponseDisplayedLengthDto : IIdentifiable
    {
        public Guid Id { get; set; }

        public long ResponseDisplayedLength { get; set; }
    }
}
