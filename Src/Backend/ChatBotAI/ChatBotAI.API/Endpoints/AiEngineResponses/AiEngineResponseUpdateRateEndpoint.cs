using System.Diagnostics.CodeAnalysis;
using ChatBotAI.API.Configuration;
using ChatBotAI.Api.Core.Endpoints;
using ChatBotAI.Application.AiEngineResponses.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotAI.API.Endpoints.AiEngineResponses
{
    [ExcludeFromCodeCoverage]
    public class AiEngineResponseUpdateRateEndpoint : EndpointBaseAbstract.WithCommandQueryAction<AiEngineResponseUpdateRatingCommand>.Action
    {
        public AiEngineResponseUpdateRateEndpoint(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost(RouteNamesV1.AiEngineResponseRoute.UpdateAiEngineResponseRate)]
        public override async Task HandleAsync([FromBody] AiEngineResponseUpdateRatingCommand requestCommand, CancellationToken cancellationToken = default)
        {
            await _mediatorCore.Send(requestCommand, cancellationToken);
        }
    }
}
