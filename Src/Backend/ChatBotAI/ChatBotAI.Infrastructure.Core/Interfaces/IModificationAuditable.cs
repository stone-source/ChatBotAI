namespace ChatBotAI.Infrastructure.Core.Interfaces;

public interface IModificationAuditable
{
    DateTime ModifiedDateTime { get; set; }
}
