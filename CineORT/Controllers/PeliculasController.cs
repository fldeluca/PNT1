using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CineORT.Models;

namespace CineORT.Controllers
{
    public class PeliculasController : Controller
    {
        private readonly DbContext _context;

        public PeliculasController(DbContext context)
        {
            _context = context;
        }

        // GET: Peliculas
        public async Task<IActionResult> Index()
        {
            var dbContext = _context.Pelicula.Include(p => p.Clasificacion).Include(p => p.Genero);
            return View(await dbContext.ToListAsync());
        }

        // GET: Peliculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pelicula == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Pelicula
                .Include(p => p.Clasificacion)
                .Include(p => p.Genero)
                .FirstOrDefaultAsync(m => m.PeliculaId == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // GET: Peliculas/Create
        public IActionResult Create()
        {
            ViewData["ClasificacionId"] = new SelectList(_context.Clasificacion, "Id", "Nombre");
            ViewData["GeneroId"] = new SelectList(_context.Genero, "Id", "Nombre");
            return View();
        }

        // POST: Peliculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PeliculaId,Nombre,Duracion,Sinopsis,Imagen,GeneroId,ClasificacionId")] Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                if (await PeliculaDuplicada(pelicula.Nombre))
                {
                    return RedirectToAction("VistaError", "Home");
                }

                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClasificacionId"] = new SelectList(_context.Clasificacion, "Id", "Nombre", pelicula.ClasificacionId);
            ViewData["GeneroId"] = new SelectList(_context.Genero, "Id", "Nombre", pelicula.GeneroId);
            return View(pelicula);
        }

        // GET: Peliculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pelicula == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Pelicula.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            ViewData["ClasificacionId"] = new SelectList(_context.Clasificacion, "Id", "Nombre", pelicula.ClasificacionId);
            ViewData["GeneroId"] = new SelectList(_context.Genero, "Id", "Nombre", pelicula.GeneroId);
            return View(pelicula);
        }

        // POST: Peliculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PeliculaId,Nombre,Duracion,Sinopsis,Imagen,GeneroId,ClasificacionId")] Pelicula pelicula)
        {
            if (id != pelicula.PeliculaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (await PeliculaDuplicada(pelicula.Nombre))
                    {
                        return RedirectToAction("VistaError", "Home");
                    }

                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.PeliculaId))
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
            ViewData["ClasificacionId"] = new SelectList(_context.Clasificacion, "Id", "Nombre", pelicula.ClasificacionId);
            ViewData["GeneroId"] = new SelectList(_context.Genero, "Id", "Nombre", pelicula.GeneroId);
            return View(pelicula);
        }

        // GET: Peliculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pelicula == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Pelicula
                .Include(p => p.Clasificacion)
                .Include(p => p.Genero)
                .FirstOrDefaultAsync(m => m.PeliculaId == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pelicula == null)
            {
                return Problem("Entity set 'DbContext.Pelicula'  is null.");
            }
            var pelicula = await _context.Pelicula.FindAsync(id);
            if (pelicula != null)
            {
                _context.Pelicula.Remove(pelicula);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(int id)
        {
          return (_context.Pelicula?.Any(e => e.PeliculaId == id)).GetValueOrDefault();
        }

        private async Task<bool> PeliculaDuplicada(string Nombre)
        {
            var pelicula = await _context.Pelicula.Where(p => p.Nombre.ToUpper() == Nombre.ToUpper()).FirstOrDefaultAsync();

            if (pelicula == null)
            {
                return false;
            }

            return true;
        }
    }
}
