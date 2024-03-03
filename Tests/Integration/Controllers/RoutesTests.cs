using System.Net;
using System.Text;
using System.Text.Json;
using net.applicationperformance.ChatApp.Auth;
using net.applicationperformance.ChatApp.Config;
using net.applicationperformance.ChatApp.Models;
using net.applicationperformance.ChatApp.Repositories;



namespace ChatApp.Tests.Integration.Controllers;

[TestFixture]
public class RoutesTests 
{
    private ChatAppFactory<Startup>? _factory;
    private HttpClient _client;
    private Mock<IUserRepository> _mockedRepo;

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        _mockedRepo = new Mock<IUserRepository>();
        ChatAppFactory<Startup>.mockRepo = _mockedRepo;
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

    [Test]
    public async Task MustPassSignIn()
    {
        // arrange
        var id = Guid.NewGuid();
        const string userName  = "user1";
        _mockedRepo.Setup(repo => repo.Add(It.IsAny<UserDto>()))
            .Returns(id);
        
        var data = new  {
                username = userName,
                password = ""
            };
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        TestContext.WriteLine($"ID:{id},UserName:{userName}");

        // act
        var response = await _client.PostAsync("/api/TokenApi/SignIn",content);
        var body = await response.Content.ReadAsStringAsync();
        
        // assert
        TestContext.WriteLine($"status: {response.StatusCode} body: {body}");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        var responseData = JsonSerializer.Deserialize<TokenResponse>(body);
        Assert.NotNull(responseData);

        var tokenId = Token.ExtractId(responseData?.token);
        Assert.That(tokenId,Is.EqualTo(id));



    }
    
    
}