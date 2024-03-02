using System.Xml;
using Microsoft.AspNetCore.Identity;
namespace net.applicationperformance.ChatApp.BusinessLogic;
using Auth;
using Models;
using Repositories;
    

public class Users(IUserRepository repo)
{
    public string SignIn(string userName, string email, string password)
    {
        var id = repo.Add(new UserDto(userName, email, password));
        return Token.ComputeToken(id, userName);
    }
    
}