using ChatbotAI.Core.Handlers;
using ChatBotAI.Application.AiEngineResponses.Commands;
using ChatBotAI.Infrastructure.Abstract;

namespace ChatBotAI.Application.AiEngineResponses.Handlers
{
    public class AIEngineResponseUpdateRateHandler : HandlerCore<AiEngineResponseUpdateRatingCommand>
    {
        private readonly IAiEngineResponseRepository _responseRepository;

        public AIEngineResponseUpdateRateHandler(IAiEngineResponseRepository repository)
        {
            _responseRepository = repository;
        }

        public override async Task Handle(AiEngineResponseUpdateRatingCommand request, CancellationToken cancellationToken)
        {
            await _responseRepository.UpdateAiEngineResponseRating(request.ObjectToSave.Id, request.ObjectToSave.Rating);
        }
    }
}
