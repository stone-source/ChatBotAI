using ChatBotAI.Domain.Abstract;
using Microsoft.Extensions.Configuration;

namespace ChatBotAI.Domain.Entities
{
    public class FakeAiStrategy : AiStrategyCore
    {
        protected override string ConfigurationApiKeyPath => String.Empty;

        public override void AddClient(string clientApiKey)
        {
        }

        public override void AddSystemChatMessage()
        {
            GetSystemPrompt();
        }

        public override void AddUserChatMessage(string userRequest)
        {
        }

        protected override string GetSystemPrompt()
        {
            return String.Empty;
        }

        public override async IAsyncEnumerable<string> CallClientAndReturnResponse()
        { 
            const string fakeResponse = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas id pretium massa."
                                  + " Morbi auctor malesuada felis, at dignissim nunc mattis ut. Vestibulum volutpat est gravida suscipit vehicula."
                                  + " Ut in tellus placerat, malesuada tellus vitae, malesuada lacus. Mauris eleifend accumsan hendrerit."
                                  + " Sed diam eros, varius eget turpis eu, luctus egestas eros. Praesent sodales mauris orci, et feugiat mi viverra sit amet."
                                  + " Phasellus laoreet tincidunt pretium. Suspendisse potenti. Aenean vitae sagittis velit, vel lacinia leo. Etiam maximus sem at fermentum efficitur."
                                  + " Donec urna elit, suscipit vel dapibus a, elementum vel dui.Mauris et volutpat sapien. Vestibulum suscipit dignissim ullamcorper"
                                  + " Donec dignissim consectetur ante, sed feugiat est ultricies nec. Donec lacinia dui elit, ut tincidunt leo rutrum ac. In vitae ullamcorper purus."
                                  + " Nunc in nulla at libero porttitor sagittis. Proin mattis odio ac ipsum mattis vestibulum. Etiam sit amet feugiat magna."
                                  + " Aenean convallis est egestas neque tincidunt sollicitudin. Aliquam ligula justo, consectetur imperdiet magna vitae, porttitor scelerisque mauris."
                                  + " Donec congue iaculis felis, sit amet maximus ante. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus."
                                  + " Nulla a eros at urna lobortis aliquet.Donec congue, mi id ultricies porta, nunc magna ornare enim, eget pretium dui nibh id mi. Sed aliquam aliquet enim, ac egestas tellus."
                                  + " Aenean ullamcorper, arcu vitae rhoncus convallis, tellus tellus tincidunt quam, eu sagittis lacus felis ac dui. Aliquam efficitur est metus, sed dictum nunc iaculis sed."
                                  + " Sed cursus lobortis erat eget ultrices. Sed elit nisi, consectetur eget tincidunt sed, rhoncus in enim. Integer sed massa eget nisi finibus molestie."
                                  + " Ut varius, lacus id condimentum suscipit, quam nisl mollis velit, ornare hendrerit orci erat et odio."
                                  + " Quisque malesuada, nunc vitae placerat venenatis, diam nisl ultrices tellus, vel malesuada lorem eros eu justo. Vestibulum faucibus lacinia scelerisque."
                                  + " Sed sit amet enim ligula. Quisque tempus enim ac fermentum scelerisque.Nullam cursus lacus ut aliquam vestibulum. Nam ornare sed urna vitae sollicitudin."
                                  + " Cras varius tincidunt odio eget tincidunt. Aenean ultricies erat at est dignissim, eu facilisis odio fringilla. Sed cursus bibendum eros et egestas."
                                  + " Curabitur non turpis arcu. Nam dui ipsum, viverra a ligula quis, pharetra semper sapien. Curabitur cursus luctus ipsum luctus dictum."
                                  + "Duis interdum finibus nulla sed vulputate. Nullam vel ex nec tellus scelerisque posuere ac eget dolor."
                                  + " Duis tincidunt, sapien vel consequat euismod, turpis urna ullamcorper quam, a viverra odio nulla nec turpis. Aliquam tellus odio, lobortis eget risus et, suscipit aliquet justo."
                                  + " Nulla porta, nunc eget aliquam malesuada, nulla arcu facilisis velit, non malesuada mi metus a justo."
                                  + " Sed quis est sodales, sollicitudin eros tempor, tristique elit. Etiam ac euismod odio.";

            var responseSentences = fakeResponse.Split('.');
            var responseSentencesRandomSize = new Random().Next(1, responseSentences.Length);

            for (int i = 0; i < responseSentencesRandomSize; i++)
            {
                if (_cancellationToken.IsCancellationRequested)
                {
                    yield break;
                }

                yield return responseSentences[i].Trim() + ".";
                await Task.Delay(100);
            }
        }

        public FakeAiStrategy(IConfiguration configuration, CancellationToken cancellationToken) : base(configuration, cancellationToken)
        {
        }
    }
}
