using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace ChatBotAI.Application.Core.Commands
{
    [ExcludeFromCodeCoverage]
    public abstract class CommandsCoreAbstract : IRequest
    {
    }

    [ExcludeFromCodeCoverage]
    public abstract class CommandsCore<TOutput> : IRequest<TOutput>
    {
    }

    [ExcludeFromCodeCoverage]
    public abstract class CommandsCoreAsync<TOutput> : IStreamRequest<TOutput>
    {
    }
}
