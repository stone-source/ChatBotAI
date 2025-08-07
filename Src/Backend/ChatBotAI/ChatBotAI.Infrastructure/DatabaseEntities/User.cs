using ChatBotAI.Infrastructure.Core.Interfaces;

namespace ChatBotAI.Infrastructure.DatabaseEntities;

public partial class User : IIdentifiable
{
    public Guid Id { get; set; }

    public string UserLogin { get; set; } = null!;

    public DateTime? CreatedDatetime { get; set; }

    public DateTime? ModifiedDatetime { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<AiEngineResponse> Aiengineresponses { get; set; } = new List<AiEngineResponse>();

    public virtual ICollection<UserRequest> Userrequests { get; set; } = new List<UserRequest>();
}
