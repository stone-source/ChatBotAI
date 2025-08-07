using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotAI.Api.Core.Endpoints
{
    [ExcludeFromCodeCoverage]
    public abstract class EndpointCore(IMediator mediator) : ControllerBase
    {
        protected IMediator _mediatorCore = mediator;
    }
}
