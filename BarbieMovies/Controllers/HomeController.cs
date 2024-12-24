using Microsoft.AspNetCore.Mvc;

namespace BarbieMovies.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
