using Gibbon.AI.Interfaces.Purpose;
using Microsoft.Extensions.DependencyInjection;

namespace Gibbon.Tests.Services.Chat;

public class General : Entry
{
    #region Test Data

    private string testMessage = "How many Rs are in strawberry";
    
    #endregion
    
    private readonly IChat[] _chatServices;
    
    public General()
    {
        _chatServices = this.ServiceProvider.GetServices<IChat>().ToArray();
    }

    [Fact]
    public void SendMessage()
    {
        foreach (var chatService in _chatServices)
        {
            var response = chatService.SendMessage(testMessage);
            
            Console.WriteLine(response);
            
            Assert.False(string.IsNullOrWhiteSpace(response));
        }
    }
}