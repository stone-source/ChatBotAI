using ChatBotAI.Api.Core.Endpoints;
using ChatBotAI.API.Configuration;
using ChatBotAI.Application.AiEngineResponses.Commands;
using ChatBotAI.Application.AiEngineResponses.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotAI.API.Endpoints.AiEngineResponses
{
    public class AiEngineResponseSaveResponseEndpoint : EndpointBaseAbstract.WithCommandQueryAction<AiEngineResponseDetailsDto>.Action
    {
        public AiEngineResponseSaveResponseEndpoint(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost(RouteNamesV1.AiEngineResponseRoute.SaveAiEngineResponse)]
        public override async Task HandleAsync([FromBody] AiEngineResponseDetailsDto request, CancellationToken cancellationToken = default)
        {
            var requestCommand = new AiEngineResponseSaveResponseCommand()
            {
                ObjectToSave = request
            };

            await _mediatorCore.Send(requestCommand, cancellationToken);
        }
    }
}
