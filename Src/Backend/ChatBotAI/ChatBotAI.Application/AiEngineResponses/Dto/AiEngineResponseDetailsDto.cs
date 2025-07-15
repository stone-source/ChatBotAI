using System.Diagnostics.CodeAnalysis;
using ChatBotAI.Application.UserRequests.Dto;
using ChatbotAI.Core.Interfaces;

namespace ChatBotAI.Application.AiEngineResponses.Dto
{
    [ExcludeFromCodeCoverage]
    public sealed class AiEngineResponseDetailsDto : IIdentifiable, ICreationAuditable, IModificationAuditable
    {
        public Guid Id { get; set; }
        
        public Guid UserRequestId { get; set; }

        public UserRequestDetailsDto UserRequest { get; set; }

        public string? Answer { get; set; }
        
        public bool? Rating { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }
    }
}
