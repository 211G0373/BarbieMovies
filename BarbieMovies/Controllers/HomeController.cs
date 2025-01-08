using BarbieMovies.Models;
using BarbieMovies.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarbieMovies.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(BarbiemoviesContext context)
        {
            Context = context;
        }

        public BarbiemoviesContext Context { get; }

        public IActionResult Index()
        {
            var Cat = Context.Categories.Include(x => x.Movies).Where(x=>x.Movies.Any()).OrderBy(x => x.Name);
            return View(Cat);
        }
        [Route("Pelicula/{nombre}")]
        public IActionResult Pelicula(string nombre)
        {
            nombre = nombre.Replace("-", " ");
            var pelicula = Context.Movies.Include(x=>x.Category).FirstOrDefault(x=>x.Title == nombre);
            return View(pelicula);
        }

        [Route("Buscar/")]
        public IActionResult Buscar(string busqueda)
        {
            busqueda = busqueda.Replace("-", " ");
            var peliculas = Context.Movies.Where(x => x.Title.Contains(busqueda)).OrderBy(x=>x.Title);
            BusquedaViewModel busquedaView = new BusquedaViewModel()
            {
                peliculas= peliculas,
                busqueda = busqueda
            };
            return View(busquedaView);
        }
    }
}
