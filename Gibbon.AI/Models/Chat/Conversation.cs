using System.Collections.Generic;
using System.Linq;
using OpenAI.Chat;

namespace Gibbon.AI.Models.Chat
{
    public class Conversation
    {
        public List<Message> Messages { get; set; }
        public int? MaxTokens { get; set; }
        public float Temperature { get; set; }
        public string[] StopSequences { get; set; }

        public Conversation(string systemInstruction, int? maxTokens = null,
            float temperature = 1F, string[] stopSequences = null)
        {
            this.MaxTokens = maxTokens;
            this.Temperature = temperature;
            this.StopSequences = stopSequences;

            this.Messages = new List<Message>()
            {
                new Message(Role.System, systemInstruction)
            };
        }

        public void AddMessage(Message message)
        {
            if (message.Role != Role.System)
            {
                this.Messages.Add(message);
            }
        }

        public void SetSystemInstruction(string systemInstruction)
        {
            this.Messages[0] = new Message(Role.System, systemInstruction);
        }

        public ChatCompletionOptions GetOpenAIConfig()
        {
            return new ChatCompletionOptions()
            {
                MaxTokens = this.MaxTokens,
                Temperature = this.Temperature,
                //StopSequences = this.StopSequences,
            };
        }
    }
}