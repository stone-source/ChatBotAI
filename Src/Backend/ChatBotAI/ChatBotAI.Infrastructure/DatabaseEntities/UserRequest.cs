using ChatBotAI.Infrastructure.Core.Interfaces;

namespace ChatBotAI.Infrastructure.DatabaseEntities;

public partial class UserRequest : IIdentifiable
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Request { get; set; } = null!;

    public DateTime? CreatedDatetime { get; set; }

    public virtual ICollection<AiEngineResponse> Aiengineresponses { get; set; } = new List<AiEngineResponse>();

    public virtual User User { get; set; } = null!;
}
