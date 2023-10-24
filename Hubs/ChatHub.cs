using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace net.applicationperformance.ChatApp.Hubs;
using Repositories;
using Auth;
public class ChatHub : Hub
{
    private readonly UserRepository _users;
    public ChatHub(UserRepository repo)
    {
        _users = repo;
    }

    public async Task JoinChatHub(string token)
    {
        Console.WriteLine($"Join request from {token}.");
        var id = Token.ExtractId(token);
        var user = _users.Get(id);
        if (id == Guid.Empty || user == null || !Token.ValidateToken(user.UserName, token))
        {
            Console.WriteLine($"Connection {Context.ConnectionId} aborting...");
            Context.Abort();
            return;
        }
        var principal = Context.User as ClaimsPrincipal;
        var identity =  principal?.Identity as ClaimsIdentity;
        identity?.AddClaim(new Claim("id",id.ToString()));
        var message = $"{user?.UserName} has joined.";
        await Clients.All.SendAsync("ReceiveMessage", "", message);
    }
    
    public async Task SendMessage(string message)
    {
        var identity = Context.User?.Identity as ClaimsIdentity;
        var id = identity?.Claims.First().Value;
        var user = _users.Get(new Guid(id));
        if (user == null)
        {
            Context.Abort();
            return;
        }
        await Clients.All.SendAsync("ReceiveMessage", user?.UserName, message);
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return Task.CompletedTask;
    }
}