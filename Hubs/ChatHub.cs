using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
namespace net.applicationperformance.ChatApp.Hubs;
using Repositories;
using Auth;

public class ChatHub(IUserRepository repo) : Hub
{
    public async Task JoinChatHub(string token)
    {
        Console.WriteLine($"Join request from {token}.");
        var id = Token.ExtractId(token);
        var user = repo.Get(id);
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
        if (id == null)
        {
            Context.Abort();
            return;
        }
        var user = repo.Get(new Guid(id));
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