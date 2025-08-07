using System.Diagnostics.CodeAnalysis;

namespace ChatBotAI.Application.Core.Queries
{
    [ExcludeFromCodeCoverage]
    public class GetMultipleByIdQueryCore<TObjectDto> : QueriesCore<ICollection<TObjectDto>>
        where TObjectDto : class
    {
        public Guid Id { get; set; }
    }
}
