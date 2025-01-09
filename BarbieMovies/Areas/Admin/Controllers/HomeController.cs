using BarbieMovies.Models;
using BarbieMovies.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarbieMovies.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        public BarbiemoviesContext Context { get; }

        [Route("/admin/home/index")]
        [Route("/admin/home")]
        [Route("/admin")]
        public IActionResult Index()
        {
            return View();
        }
        public HomeController(BarbiemoviesContext context)
        {
            Context = context;
        }
        [Route("/admin/Buscar/")]
        public IActionResult Buscar(string busqueda)
        {
            busqueda = busqueda.Replace("-", " ");
            var peliculas = Context.Movies.Where(x => x.Title.Contains(busqueda)).OrderBy(x => x.Title);
            BusquedaViewModel busquedaView = new BusquedaViewModel()
            {
                peliculas = peliculas,
                busqueda = busqueda
            };
            return View(busquedaView);
        }
    }
}
