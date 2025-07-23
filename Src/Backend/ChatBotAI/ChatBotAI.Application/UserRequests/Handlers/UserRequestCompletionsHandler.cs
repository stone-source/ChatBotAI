using ChatbotAI.Core.Handlers;
using ChatBotAI.Application.AiEngineResponses.Dto;
using ChatBotAI.Application.UserRequests.Commands;
using ChatBotAI.Domain.Entities;
using ChatBotAI.Infrastructure.Abstract;
using ChatBotAI.Infrastructure.DatabaseEntities;
using Microsoft.Extensions.Configuration;

namespace ChatBotAI.Application.UserRequests.Handlers
{
    public class UserRequestCompletionsHandler : HandlerCoreAsync<UserRequestCompletionsCommand, AiEngineResponseDetailsDto>
    {
        private readonly IUserRequestRepository _repository;
        private readonly IConfiguration _configuration;

        public UserRequestCompletionsHandler(IUserRequestRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async override IAsyncEnumerable<AiEngineResponseDetailsDto> Handle(UserRequestCompletionsCommand request, CancellationToken cancellationToken)
        {
            var requestId = await SaveRequestToDatabaseAsync(request.UserId, request.UserRequest);
            await foreach (var completion in PerformCompletionAsync(requestId, request.UserRequest, cancellationToken))
            {
                yield return completion;
            }
        }

        private async Task<Guid> SaveRequestToDatabaseAsync(Guid userId, string userRequest)
        {
            UserRequest userRequestToSave = new UserRequest()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Request = userRequest
            };

            await _repository.SaveUserRequestAsync(userRequestToSave);
            return await Task.FromResult(userRequestToSave.Id);
        }

        private async IAsyncEnumerable<AiEngineResponseDetailsDto> PerformCompletionAsync(Guid userRequestId, string userRequest, CancellationToken cancellationToken)
        {
            var engine = new AiEngine(new OpenAiStrategy(_configuration, cancellationToken));
            engine.BuildAiEngine();
            engine.AddUserRequestMessage(userRequest);

            await foreach (var engineResponseMessage in engine.CallClientAndReturnResponse())
            {
                yield return new AiEngineResponseDetailsDto()
                {
                    Id = Guid.NewGuid(),
                    UserRequestId = userRequestId,
                    Answer = engineResponseMessage
                };
            }
        }
    }
}
