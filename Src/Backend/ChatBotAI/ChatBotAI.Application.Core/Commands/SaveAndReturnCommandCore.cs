using System.Diagnostics.CodeAnalysis;

namespace ChatBotAI.Application.Core.Commands
{
    [ExcludeFromCodeCoverage]
    public class SaveAndReturnCommandCore<TInputObjectDto, TOutputObjectDto> : CommandsCore<TOutputObjectDto>
        where TInputObjectDto : class
        where TOutputObjectDto : class
    {
        public TInputObjectDto? ObjectToSave { get; set; }
    }
}
