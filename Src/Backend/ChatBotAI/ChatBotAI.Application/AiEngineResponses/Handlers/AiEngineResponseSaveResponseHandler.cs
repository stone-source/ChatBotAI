using ChatbotAI.Core.Handlers;
using ChatBotAI.Application.AiEngineResponses.Commands;
using ChatBotAI.Application.Configuration;
using ChatBotAI.Infrastructure.Abstract;
using ChatBotAI.Infrastructure.DatabaseEntities;

namespace ChatBotAI.Application.AiEngineResponses.Handlers
{
    public class AiEngineResponseSaveResponseHandler : HandlerCore<AiEngineResponseSaveResponseCommand>
    {
        protected readonly IAiEngineResponseRepository _repository;

        public AiEngineResponseSaveResponseHandler(IAiEngineResponseRepository repository)
        {
            _repository = repository;
        }

        public override async Task Handle(AiEngineResponseSaveResponseCommand request, CancellationToken cancellationToken)
        {
            var objectToSave = new AiEngineResponse()
            {
                Id = request.ObjectToSave.Id,
                UserId = FakeUserAuthentication.FakeAiEngineId,
                Answer = request.ObjectToSave.Answer,
                UserrequestId = request.ObjectToSave.UserRequestId
            };
            await _repository.AddAsync(objectToSave);
        }
    }
}
