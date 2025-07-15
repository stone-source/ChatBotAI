using ChatbotAI.Core.Commands;
using ChatBotAI.Application.AiEngineResponses.Dto;
using ChatBotAI.Application.UserRequests.Dto;
using System.Diagnostics.CodeAnalysis;

namespace ChatBotAI.Application.UserRequests.Commands
{
    [ExcludeFromCodeCoverage]
    public class UserRequestCompletionsCommand : SaveAndReturnCommandCore<UserRequestDetailsDto, AiEngineResponseDetailsDto>
    {
        public Guid UserId { get; set; }

        public required string UserRequest { get; set; }
    }
}
