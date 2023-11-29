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
    public class ClasificacionesController : Controller
    {
        private readonly DbContext _context;

        public ClasificacionesController(DbContext context)
        {
            _context = context;
        }

        // GET: Clasificaciones
        public async Task<IActionResult> Index()
        {
              return _context.Clasificacion != null ? 
                          View(await _context.Clasificacion.ToListAsync()) :
                          Problem("Entity set 'DbContext.Clasificacion'  is null.");
        }

        // GET: Clasificaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clasificacion == null)
            {
                return RedirectToAction("VistaError", "Home");
            }

            var clasificacion = await _context.Clasificacion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clasificacion == null)
            {
                return RedirectToAction("VistaError", "Home");
            }

            return View(clasificacion);
        }

        // GET: Clasificaciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clasificaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Clasificacion clasificacion)
        {
            if (ModelState.IsValid)
            {

                if(await ClasificacionDuplicada(clasificacion.Nombre))
                {
                    return RedirectToAction("VistaError", "Home");
                }

                _context.Add(clasificacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clasificacion);
        }

        // GET: Clasificaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clasificacion == null)
            {
                return RedirectToAction("VistaError", "Home");
            }

            var clasificacion = await _context.Clasificacion.FindAsync(id);
            if (clasificacion == null)
            {
                return RedirectToAction("VistaError", "Home");
            }
            return View(clasificacion);
        }

        // POST: Clasificaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Clasificacion clasificacion)
        {
            if (id != clasificacion.Id)
            {
                return RedirectToAction("VistaError", "Home");
            }

            

            if (ModelState.IsValid)
            {
                if (await ClasificacionDuplicada(clasificacion.Nombre))
                {
                    return RedirectToAction("VistaError", "Home");
                }


                try
                {
                    _context.Update(clasificacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClasificacionExists(clasificacion.Id))
                    {
                        return RedirectToAction("VistaError", "Home");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(clasificacion);
        }

        // GET: Clasificaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clasificacion == null)
            {
                return RedirectToAction("VistaError", "Home");
            }

            var clasificacion = await _context.Clasificacion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clasificacion == null)
            {
                return RedirectToAction("VistaError", "Home");
            }

            return View(clasificacion);
        }

        // POST: Clasificaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clasificacion == null)
            {
                return Problem("Entity set 'DbContext.Clasificacion'  is null.");
            }
            var clasificacion = await _context.Clasificacion.FindAsync(id);
            if (clasificacion != null)
            {
                _context.Clasificacion.Remove(clasificacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClasificacionExists(int id)
        {
          return (_context.Clasificacion?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task<bool> ClasificacionDuplicada(string Nombre)
        {
            var clasificacion = await _context.Clasificacion.Where(c => c.Nombre.ToUpper() == Nombre.ToUpper()).FirstOrDefaultAsync();

            if (clasificacion == null)
            {
                return false;
            }

            return true;
        }
    }
}
