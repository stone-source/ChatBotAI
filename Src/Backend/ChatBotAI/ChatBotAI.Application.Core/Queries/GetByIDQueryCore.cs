using System.Diagnostics.CodeAnalysis;
using ChatBotAI.Infrastructure.Core.Interfaces;

namespace ChatBotAI.Application.Core.Queries
{
    [ExcludeFromCodeCoverage]
    public class GetByIdQueryCore<TObjectDto> : QueriesCore<TObjectDto>
        where TObjectDto : class, IIdentifiable, new()
    {
        public Guid Id { get; set; }
    }
}
