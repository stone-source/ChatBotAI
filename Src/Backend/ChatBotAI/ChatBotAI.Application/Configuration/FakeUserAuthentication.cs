using System.Diagnostics.CodeAnalysis;

namespace ChatBotAI.Application.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class FakeUserAuthentication
    {
        public static Guid FakeSystemUserId => new Guid("2A89FCF3-27A9-481F-8437-007212BFA61E");
        public static Guid FakeAiEngineId => new Guid("3D5B7D53-B779-4A12-98FF-850594232412");
    }
}
