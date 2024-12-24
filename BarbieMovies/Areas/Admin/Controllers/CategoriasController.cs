using BarbieMovies.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarbieMovies.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CategoriasController : Controller
    {

        public CategoriasController(BarbiemoviesContext context)
        {
            Context = context;
        }

        public BarbiemoviesContext Context { get; }




        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Agregar()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Agregar(Categories c)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            return View();

        }

        [HttpPost]
        public IActionResult Editar(Categories c)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Eliminar(Categories c)
        {
            return View();
        }
    }
}
