using ChatBotAI.Application.AiEngineResponses.Dto;
using ChatBotAI.Application.UserRequests.Dto;
using System.Diagnostics.CodeAnalysis;
using ChatBotAI.Application.Core.Commands;
using MediatR;

namespace ChatBotAI.Application.UserRequests.Commands
{
    [ExcludeFromCodeCoverage]
    public class UserRequestCompletionsCommand : SaveAndReturnCommandCoreAsync<UserRequestDetailsDto, AiEngineResponseDetailsDto>, IStreamRequest<AiEngineResponseDetailsDto>
    {
        public Guid UserId { get; set; }

        public required string UserRequest { get; set; }
    }
}
