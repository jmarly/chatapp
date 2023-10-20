using Microsoft.AspNetCore.Mvc;

namespace net.applicationperformance.ChatApp.Controllers
{
    [Controller]
    [Route("/")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("/")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/About")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        [HttpGet]
        [Route("/Contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}