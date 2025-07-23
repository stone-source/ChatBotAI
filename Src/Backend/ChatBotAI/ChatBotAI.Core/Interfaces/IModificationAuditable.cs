namespace ChatbotAI.Core.Interfaces;

public interface IModificationAuditable
{
    DateTime ModifiedDateTime { get; set; }
}