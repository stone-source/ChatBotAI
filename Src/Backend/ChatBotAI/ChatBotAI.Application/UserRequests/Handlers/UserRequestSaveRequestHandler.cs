using ChatbotAI.Core.Handlers;
using ChatBotAI.Application.AiEngineResponses.Dto;
using ChatBotAI.Application.UserRequests.Commands;
using ChatBotAI.Infrastructure.Abstract;
using ChatBotAI.Infrastructure.DatabaseEntities;

namespace ChatBotAI.Application.UserRequests.Handlers
{
    public class UserRequestSaveRequestHandler : HandlerCore<UserRequestSaveRequestCommand>
    {
        private readonly IUserRequestRepository _repository;

        public UserRequestSaveRequestHandler(IUserRequestRepository repository)
        {
            _repository = repository;
        }

        public override async Task Handle(UserRequestSaveRequestCommand request, CancellationToken cancellationToken)
        {
            UserRequest objectToSave = new UserRequest()
            {
                Id = Guid.NewGuid(),
                UserId = request.ObjectToSave.UserId,
                Request = request.ObjectToSave.Request
            };

            SaveRequestToDatabase(objectToSave);
        }

        private async Task SaveRequestToDatabase(UserRequest objectToSave)
        {
            await _repository.SaveUserRequestAsync(objectToSave);
        }
    }
}
