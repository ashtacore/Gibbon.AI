using System;
using System.Collections.Generic;
using System.Linq;
using OpenAI.Chat;

namespace Gibbon.AI.Models.Chat
{
    public class Message
    {
        public Role Role { get; set; }

        public IEnumerable<Content> Content { get; set; }

        public Message(Role role, string text)
        {
            this.Role = role;
            this.Content = new[]
            {
                new Content(ContentType.Text, text)
            };
        }
        
        public Message(Role role, string text, string imageUrl)
        {
            this.Role = role;

            if (string.IsNullOrWhiteSpace(text))
            {
                text = "Describe this image";
            }
            
            this.Content = new[]
            {
                new Content(ContentType.Text, text),
                new Content(ContentType.Image, imageUrl)
            };
        }

        private ChatMessage _openAIChatMessage;
        public ChatMessage AsOpenAIChatMessage()
        {
            if (_openAIChatMessage == null)
            {
                switch (this.Role)
                {
                    case Role.Assistant:
                        _openAIChatMessage = new AssistantChatMessage(this.Content.First().Text);
                        break;
                    case Role.User:
                        _openAIChatMessage = new UserChatMessage(this.Content.First().Text);
                        break;
                    case Role.System:
                        _openAIChatMessage = new SystemChatMessage(this.Content.First().Text);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            return _openAIChatMessage;
        }
    }
}