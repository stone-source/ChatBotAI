using ChatBotAI.Infrastructure.Core.Repository;
using ChatBotAI.Infrastructure.Abstract;
using ChatBotAI.Infrastructure.DatabaseContext;
using ChatBotAI.Infrastructure.DatabaseEntities;
using Microsoft.EntityFrameworkCore;

namespace ChatBotAI.Infrastructure.Repository
{
    public class AiEngineResponseRepository : RepositoryCore<ChatbotAiContext, AiEngineResponse>, IAiEngineResponseRepository
    {
        public AiEngineResponseRepository(ChatbotAiContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<List<AiEngineResponse>> GetAllAiEngineResponsesWithRequestByUserId(Guid userId)
        {
            List<AiEngineResponse> engineResponses = await EntitiesDatabaseSet
                .Include(x => x.Userrequest)
                .Where(x => x.Userrequest.UserId == userId)
                .OrderBy(x => x.Id)
                .AsNoTracking()
                .ToListAsync();

            return engineResponses;
        }

        public async Task UpdateAiEngineResponseRating(Guid aiEngineResponseId, bool? rating)
        {
            AiEngineResponse? engineResponse = await EntitiesDatabaseSet
                .Where(x => x.Id == aiEngineResponseId)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

            if (engineResponse != null)
            {
                engineResponse.Rating = rating;
                await SaveChangesAsync();
            }
        }
    }
}
