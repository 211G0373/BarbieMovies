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



        [Route("/admin/Categorias/index")]
        [Route("/admin/Categorias")]
        public IActionResult Index()
        {
            var cat = Context.Categories.OrderBy(x => x.Name);
            return View(cat);
        }
        [Route("/admin/Categorias/Agregar")]
        [HttpGet]
        public IActionResult Agregar()
        {
            Categories c=new Categories();
            return View(c);
        }

        [HttpPost]
        public IActionResult Agregar(Categories c)
        {
            if (string.IsNullOrWhiteSpace(c.Name))
            {
                ModelState.AddModelError("", "");
            }else if (Context.Categories.Any(x => x.Name == c.Name))
            {
                ModelState.AddModelError("", "");
            }

            if (ModelState.IsValid)
            {
                Context.Add(c);
                Context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c);
        }
        [Route("/admin/Categorias/Editar/{id}")]

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var c = Context.Categories.FirstOrDefault(x => x.Id == id);
            return View(c);

        }

        [HttpPost]
        public IActionResult Editar(Categories c)
        {
            if (string.IsNullOrWhiteSpace(c.Name))
            {
                ModelState.AddModelError("", "");
            }
            else if (Context.Categories.Any(x => x.Name == c.Name && x.Id!=c.Id))
            {
                ModelState.AddModelError("", "");
            }
            if (ModelState.IsValid)
            {
                var cat = Context.Categories.FirstOrDefault(x => x.Id == c.Id);
                if (cat != null) { 
                cat.Name = c.Name;
                    Context.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return View();
        }
        [Route("/admin/Categorias/Eliminar/{id}")]

        [HttpGet]
        public IActionResult Eliminar(int id)
        {

            var c = Context.Categories.FirstOrDefault(x => x.Id == id);
            return View(c);
        }

        [HttpPost]
        public IActionResult Eliminar(Categories c)
        {
            var cat = Context.Categories.FirstOrDefault(x => x.Id == c.Id);
            if (cat != null)
            {
                Context.Remove(cat);
                Context.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View();
        }
    }
}
