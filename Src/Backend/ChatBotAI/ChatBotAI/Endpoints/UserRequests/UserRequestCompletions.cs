using ChatbotAI.Core.Endpoints;
using ChatBotAI.API.Configuration;
using ChatBotAI.Application.AiEngineResponses.Dto;
using ChatBotAI.Application.Configuration;
using ChatBotAI.Application.UserRequests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotAI.API.Endpoints.UserRequests
{
    public class UserRequestCompletions : EndpointBaseAbstract.WithCommandQueryAction<string>.ReturnResponse<AiEngineResponseDetailsDto>
    {
        public UserRequestCompletions(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost(RouteNamesV1.UserRequestRoute.UserRequestCompletions)]
        public override async Task<AiEngineResponseDetailsDto> HandleAsync([FromBody] string request, CancellationToken cancellationToken = default)
        {
            var requestCommand = new UserRequestCompletionsCommand()
            {
                UserId = FakeUserAuthentication.FakeSystemUserId,
                UserRequest = request
            };

            return await _mediatorCore.Send(requestCommand, cancellationToken);
        }
    }
}
