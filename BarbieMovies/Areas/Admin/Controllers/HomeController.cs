﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarbieMovies.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        [Route("/admin/home/index")]
        [Route("/admin/home")]
        [Route("/admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
