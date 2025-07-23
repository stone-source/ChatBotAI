using System.Diagnostics.CodeAnalysis;
using ChatBotAI.Application.AiEngineResponses.Dto;
using ChatbotAI.Core.Queries;
using MediatR;

namespace ChatBotAI.Application.Conversations.Queries
{
    [ExcludeFromCodeCoverage]
    public class ConversationGetAllByUserIdQuery : GetMultipleByIdQueryCore<AiEngineResponseDetailsDto>
    {
    }
}
