using System.Diagnostics.CodeAnalysis;

namespace ChatBotAI.Application.Core.Commands
{
    [ExcludeFromCodeCoverage]
    public class SaveCommandCore<TInputObjectDto> : CommandsCoreAbstract
        where TInputObjectDto : class
    {
        public TInputObjectDto? ObjectToSave { get; set; }
    }
}
