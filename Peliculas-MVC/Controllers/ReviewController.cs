using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pelicula_MVC.Data;
using Peliculas_MVC.Models;

namespace Peliculas_MVC.Controllers
{
    public class ReviewController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly PeliculaDbContext _context;

        public ReviewController(UserManager<Usuario>userManager, PeliculaDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: ReviewController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReviewController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReviewController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReviewController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReviewCreateViewModel review)
        {
            try
            {
                review.UsuarioId = _userManager.GetUserId(User);

                // Verificar si el usuario ya ha realizado una reseña para la película
                var reviewExistente = _context.Reviews
                    .FirstOrDefault(r => r.PeliculaId == review.PeliculaId && r.UsuarioId == review.UsuarioId);

                if(reviewExistente != null)
                {
                    TempData["Error"] = "Ya has realizado una reseña para esta película.";
                    return RedirectToAction("Details", "Home", new { id = review.PeliculaId });
                }

                if (ModelState.IsValid)
                {
                    var nuevaReview = new Review
                    {
                        PeliculaId = review.PeliculaId,
                        UsuarioId = review.UsuarioId,
                        Rating = review.Rating,
                        Comentario = review.Comentario,
                        FechaReview = DateTime.Now
                    };
                    _context.Reviews.Add(nuevaReview);
                    _context.SaveChanges();
                    return RedirectToAction("Details", "Home", new { id = review.PeliculaId });
                }


                return View(review);

            }
            catch
            {
                return View(review);
            }
        }

        // GET: ReviewController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReviewController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReviewController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReviewController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
