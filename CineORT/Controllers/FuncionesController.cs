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
    public class FuncionesController : Controller
    {
        private readonly DbContext _context;

        public FuncionesController(DbContext context)
        {
            _context = context;
        }

        // GET: Funciones
        public async Task<IActionResult> Index()
        {
            var dbContext = _context.Funcion.Include(f => f.Pelicula).Include(f => f.Sala);
            return View(await dbContext.ToListAsync());
        }

        // GET: Funciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Funcion == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funcion
                .Include(f => f.Pelicula)
                .Include(f => f.Sala)
                .FirstOrDefaultAsync(m => m.FuncionId == id);
            if (funcion == null)
            {
                return NotFound();
            }

            return View(funcion);
        }

        // GET: Funciones/Create
        public IActionResult Create()
        {
            ViewData["PeliculaId"] = new SelectList(_context.Pelicula, "PeliculaId", "Nombre");
            ViewData["SalaId"] = new SelectList(_context.Sala, "Id", "Nombre");
            return View();
        }

        // POST: Funciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FuncionId,HorarioFuncion,AsientosDisponibles,IsLlena,SalaId,PeliculaId")] Funcion funcion)
        {
            if (ModelState.IsValid)
            {
                Sala? sala = await _context.Sala.FindAsync(funcion.SalaId);
              

                if (sala != null)
                {
                    funcion.Sala = sala;
                    funcion.AsientosDisponibles = sala.Capacidad;
                } else
                {
                    funcion.AsientosDisponibles = 0;
                }

                _context.Add(funcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PeliculaId"] = new SelectList(_context.Pelicula, "PeliculaId", "Nombre", funcion.PeliculaId);
            ViewData["SalaId"] = new SelectList(_context.Sala, "Id", "Nombre", funcion.SalaId);
            return View(funcion);
        }


        // GET: Funciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Funcion == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funcion
                .Include(f => f.Pelicula)
                .Include(f => f.Sala)
                .FirstOrDefaultAsync(m => m.FuncionId == id);
            if (funcion == null)
            {
                return NotFound();
            }

            return View(funcion);
        }

        // POST: Funciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Funcion == null)
            {
                return Problem("Entity set 'DbContext.Funcion'  is null.");
            }
            var funcion = await _context.Funcion.FindAsync(id);
            if (funcion != null)
            {
                _context.Funcion.Remove(funcion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionExists(int id)
        {
          return (_context.Funcion?.Any(e => e.FuncionId == id)).GetValueOrDefault();
        }
    }
}
