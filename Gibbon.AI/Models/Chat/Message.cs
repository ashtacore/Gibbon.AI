using System.Collections.Generic;

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
    }
}