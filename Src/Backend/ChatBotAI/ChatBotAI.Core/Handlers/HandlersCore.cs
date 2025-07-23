using MediatR;

namespace ChatbotAI.Core.Handlers
{
    public abstract class HandlerCore<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }

    public abstract class HandlerCoreAsync<TRequest, TResponse> : IStreamRequestHandler<TRequest, TResponse>
        where TRequest : IStreamRequest<TResponse>
    {
        public abstract IAsyncEnumerable<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }

    public abstract class HandlerCore<TRequest> : IRequestHandler<TRequest>
        where TRequest : IRequest
    {
        public abstract Task Handle(TRequest request, CancellationToken cancellationToken);
    }
}
