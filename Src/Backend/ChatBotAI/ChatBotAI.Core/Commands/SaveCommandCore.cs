using System.Diagnostics.CodeAnalysis;

namespace ChatbotAI.Core.Commands
{
    [ExcludeFromCodeCoverage]
    public class SaveCommandCore<TInputObjectDto> : CommandsCoreAbstract
        where TInputObjectDto : class
    {
        public TInputObjectDto? ObjectToSave { get; set; }
    }
}
