using ChatbotAI.Core.Endpoints;
using ChatBotAI.API.Configuration;
using ChatBotAI.Application.AiEngineResponses.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace ChatBotAI.API.Endpoints.AiEngineResponses
{
    [ExcludeFromCodeCoverage]
    public class AiEngineResponseUpdateResponseDisplayedLengthEndpoint : EndpointBaseAbstract.WithCommandQueryAction<AiEngineResponseUpdateResponseDisplayedLengthCommand>.Action
    {
        public AiEngineResponseUpdateResponseDisplayedLengthEndpoint(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost(RouteNamesV1.AiEngineResponseRoute.UpdateAiEngineResponseDisplayedLength)]
        public override async Task HandleAsync([FromBody] AiEngineResponseUpdateResponseDisplayedLengthCommand requestCommand, CancellationToken cancellationToken = default)
        {
            await _mediatorCore.Send(requestCommand, cancellationToken);
        }
    }
}
