using ChatBotAI.Application.AiEngineResponses.Dto;
using ChatBotAI.Application.Conversations.Queries;
using ChatBotAI.Application.Core.Handlers;
using ChatBotAI.Application.UserRequests.Dto;
using ChatBotAI.Infrastructure.Abstract;

namespace ChatBotAI.Application.Conversations.Handlers
{
    public class ConversationGetAllByUserIdHandler : HandlerCore<ConversationGetAllByUserIdQuery, ICollection<AiEngineResponseDetailsDto>>
    {
        private readonly IAiEngineResponseRepository _repository;

        public ConversationGetAllByUserIdHandler(IAiEngineResponseRepository repository)
        { 
            _repository = repository;
        }

        public override async Task<ICollection<AiEngineResponseDetailsDto>> Handle(ConversationGetAllByUserIdQuery query, CancellationToken cancellationToken = default)
        {
            List<AiEngineResponseDetailsDto> result = new();
            var allAiEngineResponsesWithRequest = await _repository.GetAllAiEngineResponsesWithRequestByUserId(query.Id);
            foreach (var aiEngineResponseWithRequest in allAiEngineResponsesWithRequest)
            {
                result.Add(new AiEngineResponseDetailsDto()
                {
                    Id = aiEngineResponseWithRequest.Id,
                    Answer = aiEngineResponseWithRequest.Answer,
                    Rating = aiEngineResponseWithRequest.Rating,
                    CreatedDateTime = aiEngineResponseWithRequest.CreatedDatetime ?? DateTime.UtcNow,
                    ModifiedDateTime = aiEngineResponseWithRequest.ModifiedDatetime ?? DateTime.UtcNow,
                    UserRequestId = aiEngineResponseWithRequest.UserrequestId,
                    UserRequest = new UserRequestDetailsDto()
                    {
                        Id = aiEngineResponseWithRequest.Userrequest.Id,
                        UserId = aiEngineResponseWithRequest.Userrequest.UserId,
                        Request = aiEngineResponseWithRequest.Userrequest.Request,
                        CreatedDateTime = aiEngineResponseWithRequest.Userrequest.CreatedDatetime ?? DateTime.UtcNow
                    }
                });
            }
            return result;
        }
    }
}
