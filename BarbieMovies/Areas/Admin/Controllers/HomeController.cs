﻿using Microsoft.AspNetCore.Mvc;

namespace BarbieMovies.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
