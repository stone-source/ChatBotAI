using System.Security.Cryptography.X509Certificates;
using ChatbotAI.Core.Handlers;
using ChatBotAI.Application.AiEngineResponses.Commands;
using ChatBotAI.Infrastructure.Abstract;

namespace ChatBotAI.Application.AiEngineResponses.Handlers
{
    public class AiEngineResponseUpdateResponseDisplayedLengthHandler : HandlerCore<AiEngineResponseUpdateResponseDisplayedLengthCommand>
    {
        private readonly IAiEngineResponseRepository _repository;

        public AiEngineResponseUpdateResponseDisplayedLengthHandler(IAiEngineResponseRepository repository)
        { 
            _repository = repository;
        }

        public override async Task Handle(AiEngineResponseUpdateResponseDisplayedLengthCommand command, CancellationToken cancellationToken)
        {
            var objectToUpdate = await _repository.GetByIdAsync(command.ObjectToSave!.Id);

            if (objectToUpdate != null)
            {
                objectToUpdate.DisplayedAnswerLength = command.ObjectToSave.ResponseDisplayedLength;
                await _repository.SaveChangesAsync();
            }
        }
    }
}
