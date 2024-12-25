using BarbieMovies.Areas.Admin.Models;
using BarbieMovies.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarbieMovies.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class PeliculasController : Controller
    {
        public BarbiemoviesContext Context { get; }

        public PeliculasController(BarbiemoviesContext context)
        {
            Context = context;
        }
        [Route("/admin/Categorias/index")]
        [Route("/admin/Categorias")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("/admin/Categorias/Agregar")]
        [HttpGet]
        public IActionResult Agregar()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Agregar(AgregarViewModel p)
        {
            return View(p);
        }

        [Route("/admin/Categorias/Editar/{id}")]
        [HttpGet]
        public IActionResult Editar(int id)
        {
            return View();
        }
        [HttpGet]
        public IActionResult Editar(AgregarViewModel p)
        {
            return View(p);
        }


        [Route("/admin/Categorias/Eliminar/{id}")]
        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            return View();
        }
        [HttpGet]
        public IActionResult Eliminar(AgregarViewModel p)
        {
            return View(p);
        }


    }
}
