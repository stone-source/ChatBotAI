using System.Diagnostics.CodeAnalysis;

namespace ChatbotAI.Core.Queries
{
    [ExcludeFromCodeCoverage]
    public class GetMultipleByIdQueryCore<TObjectDto> : QueriesCore<ICollection<TObjectDto>>
        where TObjectDto : class
    {
        public Guid Id { get; set; }
    }
}
