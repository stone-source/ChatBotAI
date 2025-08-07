using ChatBotAI.Infrastructure.Core.Interfaces;
using ChatBotAI.Infrastructure.DatabaseEntities;

namespace ChatBotAI.Infrastructure.Abstract;

public interface IUserRequestRepository : IRepositoryCore<IDBContextCore, UserRequest>
{
    Task SaveUserRequestAsync(UserRequest userRequest);
}
