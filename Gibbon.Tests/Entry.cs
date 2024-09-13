using Gibbon.AI.Interfaces.Purpose;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gibbon.Tests;

public class Entry
{
    public ServiceProvider ServiceProvider { get; set; }
    
    private string _configFile => Path.Combine(Environment.CurrentDirectory, @"AppSettings.json");

    public Entry()
    {
        if (File.Exists(_configFile) == false)
        {
            Console.WriteLine("No config file found");
            Environment.Exit(1);
        }
        
        var configBuilder = new ConfigurationBuilder();
        configBuilder.AddJsonFile(_configFile, false, true);

        ServiceProvider = new ServiceCollection()
            .AddSingleton<IConfiguration>(configBuilder.Build())
            .AddLogging()

            .AddSingleton<IChat, Gibbon.AI.Services.Chat.OpenAI>()
            
            .BuildServiceProvider();
    }
}