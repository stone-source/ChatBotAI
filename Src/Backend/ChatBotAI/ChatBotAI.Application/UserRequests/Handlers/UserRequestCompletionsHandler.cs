using ChatbotAI.Core.Handlers;
using ChatBotAI.Application.AiEngineResponses.Dto;
using ChatBotAI.Application.Configuration;
using ChatBotAI.Application.UserRequests.Commands;
using ChatBotAI.Domain.Entities;
using ChatBotAI.Infrastructure.Abstract;
using ChatBotAI.Infrastructure.DatabaseEntities;
using Microsoft.Extensions.Configuration;

namespace ChatBotAI.Application.UserRequests.Handlers
{
    public class UserRequestCompletionsHandler : HandlerCore<UserRequestCompletionsCommand, AiEngineResponseDetailsDto>
    {
        private readonly IUserRequestRepository _userRequestrepository;
        private readonly IAiEngineResponseRepository _aiEngineRepository;
        private readonly IConfiguration _configuration;

        public UserRequestCompletionsHandler(
            IUserRequestRepository userRequestrepository,
            IAiEngineResponseRepository aiEngineRepository,
            IConfiguration configuration)
        {
            _userRequestrepository = userRequestrepository;
            _aiEngineRepository = aiEngineRepository;
            _configuration = configuration;
        }

        public override async Task<AiEngineResponseDetailsDto> Handle(UserRequestCompletionsCommand request, CancellationToken cancellationToken)
        {
            var requestId = await SaveRequestToDatabaseAsync(request.UserId, request.UserRequest);
            var completionResult = await PerformCompletionAsync(requestId, request.UserRequest);
            await SaveResponseToDatabaseAsync(completionResult);

            return await Task.FromResult(completionResult);
        }

        private async Task<Guid> SaveRequestToDatabaseAsync(Guid userId, string userRequest)
        {
            UserRequest userRequestToSave = new UserRequest()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Request = userRequest
            };

            await _userRequestrepository.SaveUserRequestAsync(userRequestToSave);
            return await Task.FromResult(userRequestToSave.Id);
        }

        private async Task<AiEngineResponseDetailsDto> PerformCompletionAsync(Guid userRequestId, string userRequest)
        {
            var engine = new AiEngine(new OpenAiStrategy(_configuration));
            engine.BuildAiEngine();
            engine.AddUserRequestMessage(userRequest);
            var engineResponseMessage = engine.CallClientAndReturnResponse();
            var objectToReturn = new AiEngineResponseDetailsDto()
            {
                Id = Guid.NewGuid(),
                UserRequestId = userRequestId,
                Answer = engineResponseMessage
            };

            return await Task.FromResult(objectToReturn);
        }

        private async Task SaveResponseToDatabaseAsync(AiEngineResponseDetailsDto response)
        {
            var objectToSave = new AiEngineResponse()
            {
                Id = response.Id,
                UserId = FakeUserAuthentication.FakeAiEngineId,
                Answer = response.Answer,
                UserrequestId = response.UserRequestId
            };

            await _aiEngineRepository.AddAsync(objectToSave);
            await _aiEngineRepository.SaveChangesAsync();
        }
    }
}
