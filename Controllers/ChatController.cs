using Microsoft.AspNetCore.Mvc;

namespace net.applicationperformance.ChatApp.Controllers;
using Repositories;
using Models;

[Route("/Chat")]
[Controller]
public class ChatController : Controller
{
    private readonly IRepositoryService<Guid,UserDto> _userRepository;

    public ChatController(UserRepository repo)
    {
        _userRepository = repo;
    }
    
    [HttpPost]
    [Route("/Chat/Messages")]
    public ActionResult Messages(string userName, string token)
    {
        ViewBag.token = token;
        return View();
    }
 
}