﻿using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace ChatbotAI.Core.Queries
{
    [ExcludeFromCodeCoverage]
    public abstract class QueriesCore : IRequest
    {
    }

    [ExcludeFromCodeCoverage]
    public abstract class QueriesCore<TResponse> : IRequest<TResponse>
    {
    }
}
