using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using BarbieMovies.Models;

namespace BarbieMovies.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(BarbiemoviesContext context)
        {
            Context = context;
        }

        public BarbiemoviesContext Context { get; }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string user, string password)
        {
            var usuario = Context.Usuarios.SingleOrDefault(x => x.Usuario == user && x.Password == password);
            if (usuario != null)
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, usuario.Nombre));
                claims.Add(new Claim(ClaimTypes.Role, usuario.Rol));
                claims.Add(new Claim("Id", usuario.Id.ToString()));

                //Crear la identidad
                var identidad = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(new ClaimsPrincipal(identidad));//Permite iniciar sesión


                return RedirectToAction("Index", "Home", new { area = "Admin" });//Lugar a donde puede entrar
            }
            else
            {
                ModelState.AddModelError("", "El usuario o contraseña son incorrectos");
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();//Método que cierra sesión
            return RedirectToAction("Index", "Home");//Página principal
        }

        public IActionResult AccesDenied()
        {
            return View();
        }
    }
}
