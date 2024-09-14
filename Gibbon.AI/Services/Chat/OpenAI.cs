using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gibbon.AI.Interfaces.Purpose;
using Gibbon.AI.Models;
using Gibbon.AI.Models.Chat;
using Gibbon.AI.Structs;
using Microsoft.Extensions.Configuration;
using OpenAI.Chat;

namespace Gibbon.AI.Services.Chat
{
    public class OpenAI : IChat
    {
        private AIModel[] _availableModels = new[]
        {
            new AIModel("GPT4o Mini", "gpt-4o-mini"),
            new AIModel("GPT4o", "gpt-4o"),
            new AIModel("GPT3.5 Turbo", "gpt-3.5-turbo"),
        };
        
        private readonly IConfiguration _configuration;
        
        private ChatClient _chatClient;
        private string _apiKey => _configuration.GetSection("Keys")["OpenAI"];
        private AIModel _model;
        private Conversation _conversation;
        
        public OpenAI(IConfiguration configuration)
        {
            _configuration = configuration;

            _model = _availableModels.First();
            _chatClient = new ChatClient(_model.ApiName, _apiKey);
            _conversation = new Conversation(Defaults.DefaultChatSystemInstruction);
        }

        public async Task<string> SendMessage(string message)
        {
            _conversation.AddMessage(new Message(Role.User, message));
            
            var response =
                _chatClient.CompleteChatStreamingAsync(_conversation.Messages.Select(mess => mess.AsOpenAIChatMessage()), _conversation.GetOpenAIConfig());
            
            string ret = string.Empty;
            
            await foreach (StreamingChatCompletionUpdate update in response)
            {
                foreach (ChatMessageContentPart updatePart in update.ContentUpdate)
                {
                    ret += updatePart.Text;
                }
            }

            return ret;
        }

        public AIModel[] GetAvailableModels()
        {
            return _availableModels;
        }

        public void SelectModel(AIModel model)
        {
            _model = model;
            _chatClient = new ChatClient(_model.ApiName, _apiKey);
        }
    }
}