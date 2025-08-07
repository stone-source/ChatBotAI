using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace ChatBotAI.Api.Core.Endpoints
{
    [ExcludeFromCodeCoverage]
    public abstract class EndpointBaseAbstract
    {
        public static class WithCommandQueryAction<TCommandQueryAction>
        {
            public abstract class ReturnResponse<TResponse>(IMediator mediator) : EndpointCore(mediator)
            {
                public abstract Task<TResponse> HandleAsync(TCommandQueryAction commandQueryAction,
                    CancellationToken cancellationToken = default);
            }

            public abstract class Action(IMediator mediator) : EndpointCore(mediator)
            {
                public abstract Task HandleAsync(TCommandQueryAction commandQueryAction,
                    CancellationToken cancellationToken = default);
            }
        }

        public static class WithoutCommandQueryAction
        {
            public abstract class ReturnResponse<TResponse>(IMediator mediator) : EndpointCore(mediator)
            {
                public abstract Task<TResponse> HandleAsync(CancellationToken cancellationToken = default);
            }

            public abstract class Action(IMediator mediator) : EndpointCore(mediator)
            {
                public abstract Task HandleAsync(CancellationToken cancellationToken = default);
            }
        }
    }
}
