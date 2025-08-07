using ChatBotAI.Infrastructure.Core.Repository;
using ChatBotAI.Infrastructure.Abstract;
using ChatBotAI.Infrastructure.DatabaseContext;
using ChatBotAI.Infrastructure.DatabaseEntities;

namespace ChatBotAI.Infrastructure.Repository
{
    public class UserRequestRepository : RepositoryCore<ChatbotAiContext, UserRequest>, IUserRequestRepository
    {
        public UserRequestRepository(ChatbotAiContext databaseContext) : base(databaseContext)
        {
        }

        public async Task SaveUserRequestAsync(UserRequest userRequest)
        {
            await AddAsync(userRequest);
        }
    }
}
