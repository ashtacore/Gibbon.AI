using Gibbon.AI.Interfaces.Purpose;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Gibbon.Tests.Services.Chat;

public class General : Entry
{
    private readonly ITestOutputHelper _testOutputHelper;

    #region Test Data

    private string testMessage = "How many Rs are in strawberry";
    
    #endregion
    
    private readonly IChat[] _chatServices;
    
    public General(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _chatServices = this.ServiceProvider.GetServices<IChat>().ToArray();
    }

    [Fact]
    public async void SendMessage()
    {
        foreach (var chatService in _chatServices)
        {
            var response =  await chatService.SendMessage(testMessage);
            
            _testOutputHelper.WriteLine(response);
            
            Assert.False(string.IsNullOrWhiteSpace(response));
        }
    }
}