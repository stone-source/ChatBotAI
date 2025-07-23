using System.Diagnostics.CodeAnalysis;
using ChatbotAI.Core.Interfaces;

namespace ChatbotAI.Core.Queries
{
    [ExcludeFromCodeCoverage]
    public class GetByIdQueryCore<TObjectDto> : QueriesCore<TObjectDto>
        where TObjectDto : class, IIdentifiable, new()
    {
        public Guid Id { get; set; }
    }
}
