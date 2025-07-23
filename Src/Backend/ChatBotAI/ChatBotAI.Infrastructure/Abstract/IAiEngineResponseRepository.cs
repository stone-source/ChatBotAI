using ChatbotAI.Core.Interfaces;
using ChatBotAI.Infrastructure.DatabaseEntities;

namespace ChatBotAI.Infrastructure.Abstract;

public interface IAiEngineResponseRepository : IRepositoryCore<IDBContextCore, AiEngineResponse>
{
    Task<List<AiEngineResponse>> GetAllAiEngineResponsesWithRequestByUserId(Guid userId);

    Task UpdateAiEngineResponseRating(Guid aiEngineResponseId, bool? rating);
}