using System.Diagnostics.CodeAnalysis;
using ChatbotAI.Core.Interfaces;

namespace ChatBotAI.Application.AiEngineResponses.Dto
{
    [ExcludeFromCodeCoverage]
    public sealed class AiEngineResponseRatingDto : IIdentifiable
    {
        public Guid Id { get; set; }

        public bool? Rating { get; set; }
    }
}
