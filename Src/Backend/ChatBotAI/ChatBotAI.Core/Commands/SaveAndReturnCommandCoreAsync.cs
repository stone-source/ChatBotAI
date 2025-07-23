using System.Diagnostics.CodeAnalysis;

namespace ChatbotAI.Core.Commands
{
    [ExcludeFromCodeCoverage]
    public class SaveAndReturnCommandCoreAsync<TInputObjectDto, TOutputObjectDto> : CommandsCoreAsync<TOutputObjectDto>
        where TInputObjectDto : class
        where TOutputObjectDto : class
    {
        public TInputObjectDto? ObjectToSave { get; set; }
    }
}
