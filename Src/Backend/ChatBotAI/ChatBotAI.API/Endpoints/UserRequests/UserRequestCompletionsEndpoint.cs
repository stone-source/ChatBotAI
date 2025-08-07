using ChatBotAI.API.Configuration;
using ChatBotAI.Application.Configuration;
using ChatBotAI.Application.UserRequests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ChatBotAI.Api.Core.Endpoints;

namespace ChatBotAI.API.Endpoints.UserRequests
{
    public class UserRequestCompletionsEndpoint : EndpointBaseAsyncAbstract.WithCommandQueryAction<string>.Action
    {
        public UserRequestCompletionsEndpoint(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet(RouteNamesV1.UserRequestRoute.UserRequestCompletions)]
        public override async Task HandleAsync([FromQuery] string request, CancellationToken cancellationToken = default)
        {
            Response.Headers.ContentType = "text/event-stream";
            Response.Headers.CacheControl = "no-cache";
            Response.Headers.Connection = "keep-alive";

            var requestCommand = new UserRequestCompletionsCommand()
            {
                UserId = FakeUserAuthentication.FakeSystemUserId,
                UserRequest = request
            };

            await foreach (var response in _mediatorCore.CreateStream(requestCommand, cancellationToken))
            {
                var json = JsonSerializer.Serialize(response);
                await Response.WriteAsync($"data: {json}\n\n", cancellationToken);
                await Response.Body.FlushAsync(cancellationToken);
            }

            await Response.WriteAsync("data: [DONE]\n\n", cancellationToken);
            await Response.Body.FlushAsync(cancellationToken);
        }
    }
}
