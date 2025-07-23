using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace ChatbotAI.Core.Endpoints
{
    [ExcludeFromCodeCoverage]
    public abstract class EndpointBaseAsyncAbstract
    {
        public static class WithCommandQueryAction<TCommandQueryAction>
        {
            public abstract class ReturnResponse<TResponse>(IMediator mediator) : EndpointCore(mediator)
            {
                public abstract TResponse HandleAsync(TCommandQueryAction commandQueryAction, CancellationToken cancellationToken = default);
            }

            public abstract class Action(IMediator mediator) : EndpointCore(mediator)
            {
                public abstract Task HandleAsync(TCommandQueryAction commandQueryAction, CancellationToken cancellationToken = default);
            }
        }
    }
}
