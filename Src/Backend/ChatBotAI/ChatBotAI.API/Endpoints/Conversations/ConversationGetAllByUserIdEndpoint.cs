using System.Diagnostics.CodeAnalysis;
using ChatBotAI.API.Configuration;
using ChatBotAI.Api.Core.Endpoints;
using ChatBotAI.Application.AiEngineResponses.Dto;
using ChatBotAI.Application.Conversations.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotAI.API.Endpoints.Conversations
{
    [ExcludeFromCodeCoverage]
    public class ConversationGetAllByUserIdEndpoint : EndpointBaseAbstract.WithCommandQueryAction<Guid>.ReturnResponse<List<AiEngineResponseDetailsDto>>
    {
        public ConversationGetAllByUserIdEndpoint(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet(RouteNamesV1.ConversationRoute.GetConversationsByUserId)]
        public override async Task<List<AiEngineResponseDetailsDto>> HandleAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var requestQuery = new ConversationGetAllByUserIdQuery { Id = id };
            var result = await _mediatorCore.Send(requestQuery, cancellationToken);
            return await Task.FromResult(result.OrderBy(x => x.CreatedDateTime).ToList());
        }
    }
}
