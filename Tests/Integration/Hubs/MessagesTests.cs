using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Http.Logging;
using net.applicationperformance.ChatApp.Auth;
using net.applicationperformance.ChatApp.Config;
using net.applicationperformance.ChatApp.Models;
using net.applicationperformance.ChatApp.Repositories;
using NUnit.Framework.Constraints;

namespace ChatApp.Tests.Integration.Hubs;

[TestFixture]
public class MessagesTests
{
    private ChatAppFactory<Startup>? _factory;
    private Mock<IUserRepository> _mockedRepo;
    private HubConnection _hubConnection;
    private TaskCompletionSource<(string, string)> _messageReceived = new();


    [OneTimeSetUp]
    public void GlobalSetup()
    {
        _mockedRepo = new Mock<IUserRepository>();
        ChatAppFactory<Startup>.mockRepo = _mockedRepo;
        _factory = new ChatAppFactory<Startup>();
        
    }

    [OneTimeTearDown]
    public void GlobalTearDown()
    {
        _factory?.Dispose();
    }

    [SetUp]
    public async Task Setup()
    {
        
        _hubConnection = new HubConnectionBuilder()
            .WithUrl("/ChatHub", options =>
            {
                options.HttpMessageHandlerFactory = _ => _factory.Server.CreateHandler();
            })
            .Build();
        
        _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            _messageReceived.SetResult((user, message));
        });

        await _hubConnection.StartAsync();
    }
    [TearDown]
    public async Task TearDown()
    {
        await _hubConnection.DisposeAsync();
    }

    [Test]
    public async Task MustPassJoinChatHub()
    {
        // arrange
        
        var id = Guid.NewGuid();
        const string userName = "user1";
        var user = new UserDto( id, userName, "", "     " );
        
        // prepare the repository to return the user
        _mockedRepo.Setup(repo => repo.Get(It.IsAny<Guid>())).Returns(user);
        var token = Token.ComputeToken(id, userName);
        
        TestContext.WriteLine($"Connection: {_hubConnection.ConnectionId}");

        // act
        await _hubConnection.SendAsync("JoinChatHub", token);
        var message = await _messageReceived.Task;
        // assert
        TestContext.WriteLine($"{message.Item2}");
        Assert.That(message.Item2, Does.Contain(userName));
    }
    
    
}