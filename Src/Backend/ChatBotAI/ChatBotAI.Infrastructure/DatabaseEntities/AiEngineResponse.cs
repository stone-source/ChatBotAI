using ChatBotAI.Infrastructure.Core.Interfaces;

namespace ChatBotAI.Infrastructure.DatabaseEntities;

public partial class AiEngineResponse : IIdentifiable
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid UserrequestId { get; set; }

    public string Answer { get; set; } = null!;

    public bool? Rating { get; set; }
    
    public DateTime? CreatedDatetime { get; set; }

    public DateTime? ModifiedDatetime { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual UserRequest Userrequest { get; set; } = null!;
}
