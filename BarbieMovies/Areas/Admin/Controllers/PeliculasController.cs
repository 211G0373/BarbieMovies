using BarbieMovies.Areas.Admin.Models;
using BarbieMovies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarbieMovies.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PeliculasController : Controller
    {
        public BarbiemoviesContext Context { get; }
        public IWebHostEnvironment Host { get; }

        public PeliculasController(BarbiemoviesContext context, IWebHostEnvironment host)
        {
            Context = context;
            Host = host;
        }



        [Authorize(Roles = "Administrador")]

        [Route("/admin/Peliculas/index")]
        [Route("/admin/Peliculas")]
        public IActionResult Index()
        {
            
            return View(Context.Movies.OrderBy(x=>x.Title));
        }
        [Authorize(Roles = "Administrador")]

        [Route("/admin/Peliculas/Agregar")]
        [HttpGet]
        public IActionResult Agregar()
        {
            var cat = Context.Categories.OrderBy(x => x.Name);
            AgregarViewModel vm=new AgregarViewModel()
            {
                Categorias = cat
            };
            return View(vm);
        }
        [Authorize(Roles = "Administrador")]

        [HttpPost]
        public IActionResult Agregar(AgregarViewModel? p, IFormFile foto)
        {

            //foto

            if (foto.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("", "Solo están permitidas imagenes .jpg");
                
            }
            if (foto.Length > 1024 * 1024 * 5)
            {
                ModelState.AddModelError("", "No se permiten imagenes mayores a 5MB");
            }

            //pelicula
            if (string.IsNullOrWhiteSpace(p.Pelicula.Title)){
                ModelState.AddModelError("", "a");
            }else if (Context.Movies.Any(x => x.Title == p.Pelicula.Title))
            {
                ModelState.AddModelError("", "a");

            }
            if (DateTime.Today.Year < p.Pelicula.ReleaseYear || p.Pelicula.ReleaseYear < 1900)
            {
                ModelState.AddModelError("", "b");
            }
            if (string.IsNullOrWhiteSpace(p.Pelicula.Director))
            {
                ModelState.AddModelError("", "c");
            }
            if(p.Pelicula.DurationMinutes<=0)
            {
                ModelState.AddModelError("", "d");
            }
            if (!Context.Categories.Any(x=>x.Id== p.Pelicula.CategoryId))
            {
                ModelState.AddModelError("", "e");
            }

            if (ModelState.IsValid)
            {
                Context.Add(p.Pelicula);
                Context.SaveChanges();

                var path = Host.WebRootPath + "/img_peliculas/" + p.Pelicula.Id + ".jpg";
                FileStream fs = new FileStream(path, FileMode.Create);
                foto.CopyTo(fs);
                fs.Close();

                return RedirectToAction("Index");
            }
            p.Categorias= Context.Categories.OrderBy(x => x.Name);
            return View(p);
        }

        [Authorize(Roles = "Administrador")]

        [Route("/admin/Peliculas/Editar/{id}")]
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var cat = Context.Categories.OrderBy(x => x.Name);
            var Pelicula = Context.Movies.FirstOrDefault(x => x.Id == id);
            if (Pelicula == null)
            {
                return RedirectToAction("Index");
            }

            AgregarViewModel vm = new AgregarViewModel()
            {
                Pelicula = Pelicula,
                Categorias = cat
            };
            return View(vm);
        }
        [Authorize(Roles = "Administrador")]

        [HttpPost]
        public IActionResult Editar(AgregarViewModel p, IFormFile? foto)
        {
            if (foto != null)
            {
                if (foto.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("", "Solo están permitidas imagenes .jpg");

                }
                if (foto.Length > 1024 * 1024 * 5)
                {
                    ModelState.AddModelError("", "No se permiten imagenes mayores a 5MB");
                }
            }
           

            //pelicula
            if (string.IsNullOrWhiteSpace(p.Pelicula.Title))
            {
                ModelState.AddModelError("", "a");
            }
            else if (Context.Movies.Any(x => x.Title == p.Pelicula.Title))
            {
                ModelState.AddModelError("", "a");

            }
            if (DateTime.Today.Year < p.Pelicula.ReleaseYear || p.Pelicula.ReleaseYear < 1900)
            {
                ModelState.AddModelError("", "b");
            }
            if (string.IsNullOrWhiteSpace(p.Pelicula.Director))
            {
                ModelState.AddModelError("", "c");
            }
            if (p.Pelicula.DurationMinutes <= 0)
            {
                ModelState.AddModelError("", "d");
            }
            if (!Context.Categories.Any(x => x.Id == p.Pelicula.CategoryId))
            {
                ModelState.AddModelError("", "e");
            }

            if (ModelState.IsValid)
            {
                var PeliculaOriginal=Context.Movies.FirstOrDefault(x => x.Id == p.Pelicula.Id);
                PeliculaOriginal.Director = p.Pelicula.Director;
                PeliculaOriginal.CategoryId= p.Pelicula.CategoryId;
                PeliculaOriginal.Title = p.Pelicula.Title;
                PeliculaOriginal.ReleaseYear= p.Pelicula.ReleaseYear;
                PeliculaOriginal.DurationMinutes= p.Pelicula.DurationMinutes;

                Context.SaveChanges();

                if (foto != null)
                {
                    var path = Host.WebRootPath + "/img_peliculas/" + p.Pelicula.Id + ".jpg";
                    FileStream fs = new FileStream(path, FileMode.Create);
                    foto.CopyTo(fs);
                    fs.Close();
                }
                    

                return RedirectToAction("Index");
            }
            p.Categorias = Context.Categories.OrderBy(x => x.Name);
            return View(p);
        }


        [Authorize(Roles = "Administrador")]

        [Route("/admin/Peliculas/Eliminar/{id}")]
        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var Pelicula = Context.Movies.FirstOrDefault(x => x.Id == id);
            if (Pelicula == null)
            {
                return RedirectToAction("Index");
            }

            return View(Pelicula);
        }
        [Authorize(Roles = "Administrador")]

        [HttpPost]
        public IActionResult Eliminar(Movies p)
        {
            var Pelicula = Context.Movies.FirstOrDefault(x => x.Id == p.Id);
            if (Pelicula != null)
            {
                Context.Remove(Pelicula);
                Context.SaveChanges();
                var path = Host.WebRootPath + "/img_peliculas/" + p.Id + ".jpg";
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return RedirectToAction("Index");
            }
            return View(p);
        }


    }
}
