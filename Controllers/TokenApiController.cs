using Microsoft.AspNetCore.Mvc;

namespace net.applicationperformance.ChatApp.Controllers;
using Repositories;
using Models;
using Auth;

[Route("/api/TokenApi")]
[ApiController]
public class TokenApiController : ControllerBase
{
    private readonly IRepositoryService<Guid,UserDto> _users;
    
    public TokenApiController(UserRepository repo)
    {
        _users = repo;
    }
    
    [HttpPost]
    [Route("/api/TokenApi/SignIn")]
    public IActionResult SignIn([FromBody] TokenRequest req)
    {
        var id = _users.Add(new UserDto(req.UserName, "" ,""));
        return Ok(new
        {
            token = Token.ComputeToken(id,req.UserName)
        });
    }
}