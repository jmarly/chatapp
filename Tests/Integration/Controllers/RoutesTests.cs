using Microsoft.AspNetCore.Mvc.Testing;
using net.applicationperformance.ChatApp.Config;


namespace ChatApp.Tests.Integration.Controllers;

[TestFixture]
public class RoutesTests 
{
    private ChatAppFactory<Startup>? _factory;

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        _factory = new ChatAppFactory<Startup>();
    }

    [OneTimeTearDown]
    public void GlobalTearDown()
    {
        _factory?.Dispose();
    }

    [Test]
    public async Task MustPassRootEndpoint()
    {
        // arrange
        var client = _factory?.CreateClient();
        
        // Act
        var response = await client.GetAsync("/");
        response.EnsureSuccessStatusCode();

    }
    
}