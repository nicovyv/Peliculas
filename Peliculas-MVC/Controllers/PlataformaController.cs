
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pelicula_MVC.Data;
using Peliculas_MVC.Models;

[Authorize(Roles = "Admin")]
public class PlataformaController : Controller
{
    private readonly PeliculaDbContext _context;

    public PlataformaController(PeliculaDbContext context)
    {
        _context = context;
    }

    // GET: PLATAFORMAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Plataformas.ToListAsync());
    }

    // GET: PLATAFORMAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var plataforma = await _context.Plataformas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (plataforma == null)
        {
            return NotFound();
        }

        return View(plataforma);
    }

    // GET: PLATAFORMAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PLATAFORMAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Url,LogoUrl,PlataformaPeliculas")] Plataforma plataforma)
    {
        if (ModelState.IsValid)
        {
            _context.Add(plataforma);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(plataforma);
    }

    // GET: PLATAFORMAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var plataforma = await _context.Plataformas.FindAsync(id);
        if (plataforma == null)
        {
            return NotFound();
        }
        return View(plataforma);
    }

    // POST: PLATAFORMAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nombre,Url,LogoUrl,PlataformaPeliculas")] Plataforma plataforma)
    {
        if (id != plataforma.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(plataforma);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlataformaExists(plataforma.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(plataforma);
    }

    // GET: PLATAFORMAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var plataforma = await _context.Plataformas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (plataforma == null)
        {
            return NotFound();
        }

        return View(plataforma);
    }

    // POST: PLATAFORMAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var plataforma = await _context.Plataformas.FindAsync(id);
        if (plataforma != null)
        {
            _context.Plataformas.Remove(plataforma);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PlataformaExists(int? id)
    {
        return _context.Plataformas.Any(e => e.Id == id);
    }
}
