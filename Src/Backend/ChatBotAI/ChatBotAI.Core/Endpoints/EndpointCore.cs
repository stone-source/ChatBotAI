using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatbotAI.Core.Endpoints
{
    [ExcludeFromCodeCoverage]
    public abstract class EndpointCore(IMediator mediator) : ControllerBase
    {
        protected IMediator _mediatorCore = mediator;
    }
}
