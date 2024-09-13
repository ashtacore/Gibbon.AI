using System.Collections.Generic;

namespace Gibbon.AI.Models.Chat
{
    public class Conversation
    {
        public List<Message> Messages { get; set; }
        public int? MaxTokens { get; set; }
        public double Temperature { get; set; }
        public string[] StopSequences { get; set; }

        public Conversation(string systemInstruction, int? maxTokens = null,
            double temperature = 1.0, string[] stopSequences = null)
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
    }
}