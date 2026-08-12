using Microsoft.AspNetCore.Mvc;
using Pelicula_MVC.Data;
using Peliculas_MVC.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Peliculas_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PeliculaDbContext _context;

        public HomeController(ILogger<HomeController> logger, PeliculaDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var peliculas = await _context.Peliculas.ToListAsync();
            return View(peliculas);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
