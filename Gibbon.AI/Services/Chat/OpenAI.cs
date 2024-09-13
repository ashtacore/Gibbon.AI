using Gibbon.AI.Interfaces.Purpose;
using Microsoft.Extensions.Configuration;

namespace Gibbon.AI.Services.Chat
{
    public class OpenAI : IChat
    {
        private readonly IConfiguration _configuration;
        
        public OpenAI(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string SendMessage(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}