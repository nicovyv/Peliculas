using Microsoft.AspNetCore.Mvc;
using Pelicula_MVC.Data;
using Peliculas_MVC.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

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

        public async Task<IActionResult> Index(int page = 1, string txtBusqueda = "", int generoId = 0)
        {
            const int pageSize = 8;

            var consulta = _context.Peliculas.AsQueryable();
            if (!string.IsNullOrEmpty(txtBusqueda))
            {
                consulta = consulta.Where(p => p.Titulo.Contains(txtBusqueda));
            }

            if (generoId > 0)
            {
                consulta = consulta.Where(p => p.GeneroId == generoId);
            }


            var totalCount = await consulta.CountAsync();
            var peliculas = await consulta
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.PageNumber = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewBag.HasPreviousPage = page > 1;
            ViewBag.HasNextPage = page < ViewBag.TotalPages;
            ViewBag.Consulta = txtBusqueda;

            var generos = await _context.Generos.OrderBy(g => g.Descripcion).ToListAsync();
            generos.Insert(0, new Genero { Id = 0, Descripcion = "Todos" });

            ViewBag.GeneroId = new SelectList(
                generos,
                "Id",
                "Descripcion",
                generoId
             );

            return View(peliculas);
        }

        //Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }
            var pelicula = await _context.Peliculas
                .Include(p => p.Genero)
                .Include(p => p.ListaReviews)
                .ThenInclude(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }


            ViewBag.UserReview = false;
            if(User?.Identity?.IsAuthenticated == true && pelicula.ListaReviews != null)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ViewBag.UserReview = !(pelicula.ListaReviews.FirstOrDefault(r => r.UsuarioId == userId) == null);
            }




            return View(pelicula);
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
