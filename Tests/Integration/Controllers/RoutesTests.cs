using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using net.applicationperformance.ChatApp.Config;


namespace ChatApp.Tests.Integration.Controllers;

[TestFixture]
public class RoutesTests 
{
    private ChatAppFactory<Startup>? _factory;
    private HttpClient _client;

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        _factory = new ChatAppFactory<Startup>();
        _client = _factory.CreateClient();
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
        
        // Act
        var response = await _client.GetAsync("/");
        response.EnsureSuccessStatusCode();
    }
    [Test]
    public async Task MustFailBadEndpoint()
    {
        // arrange
        
        // Act
        var response = await _client.GetAsync("/justabaduri");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    
}