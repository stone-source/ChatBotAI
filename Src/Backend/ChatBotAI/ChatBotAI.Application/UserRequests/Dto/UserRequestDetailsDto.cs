using ChatBotAI.Infrastructure.Core.Interfaces;

namespace ChatBotAI.Application.UserRequests.Dto
{
    public class UserRequestDetailsDto : IIdentifiable, ICreationAuditable
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public required string Request { get; set; }
    }
}
