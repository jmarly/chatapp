using Microsoft.AspNetCore.Mvc;

namespace net.applicationperformance.ChatApp.Controllers;
using Models;
using Repositories;

[ApiController]
[Route("/api/TokenApi")]

public class TokenApiController : ControllerBase
{
    private readonly IUserRepository repo;

    public TokenApiController(IUserRepository repo)
    {
        this.repo = repo;
    }
    
    [HttpPost]
    [Route("/api/TokenApi/SignIn")]
    public IActionResult SignIn([FromBody] TokenRequest req)
    {
        if (req.UserName == null )
        {
            return NotFound();
        }
        var users = new BusinessLogic.Users(repo);
        var tk = users.SignIn(req.UserName, "", "");

       return Ok(new
        {
            token = tk
        });
    }
}