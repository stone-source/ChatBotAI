using System.Diagnostics.CodeAnalysis;
using ChatBotAI.Application.AiEngineResponses.Dto;
using ChatBotAI.Application.Core.Queries;

namespace ChatBotAI.Application.Conversations.Queries
{
    [ExcludeFromCodeCoverage]
    public class ConversationGetAllByUserIdQuery : GetMultipleByIdQueryCore<AiEngineResponseDetailsDto>
    {
    }
}
