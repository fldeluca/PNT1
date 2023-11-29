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
    public class GenerosController : Controller
    {
        private readonly DbContext _context;

        public GenerosController(DbContext context)
        {
            _context = context;
        }

        // GET: Generos
        public async Task<IActionResult> Index()
        {
              return _context.Genero != null ? 
                          View(await _context.Genero.ToListAsync()) :
                          Problem("Entity set 'DbContext.Genero'  is null.");
        }

        // GET: Generos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Genero == null)
            {
                return RedirectToAction("VistaError", "Home");
            }

            var genero = await _context.Genero
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genero == null)
            {
                return RedirectToAction("VistaError", "Home");
            }

            return View(genero);
        }

        // GET: Generos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Generos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Genero genero)
        {
            if (ModelState.IsValid)
            {

                if (await GeneroDuplicado(genero.Nombre))
                {
                    return RedirectToAction("VistaError", "Home");
                }
                _context.Add(genero);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genero);
        }

        // GET: Generos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Genero == null)
            {
                return RedirectToAction("VistaError", "Home");
            }

            var genero = await _context.Genero.FindAsync(id);
            if (genero == null)
            {
                return RedirectToAction("VistaError", "Home");
            }
            return View(genero);
        }

        // POST: Generos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Genero genero)
        {
            if (id != genero.Id)
            {
                return RedirectToAction("VistaError", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (await GeneroDuplicado(genero.Nombre))
                    {
                        return RedirectToAction("VistaError", "Home");
                    }

                    _context.Update(genero);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeneroExists(genero.Id))
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
            return View(genero);
        }

        // GET: Generos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Genero == null)
            {
                return RedirectToAction("VistaError", "Home");
            }

            var genero = await _context.Genero
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genero == null)
            {
                return RedirectToAction("VistaError", "Home");
            }

            return View(genero);
        }

        // POST: Generos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Genero == null)
            {
                return Problem("Entity set 'DbContext.Genero'  is null.");
            }
            var genero = await _context.Genero.FindAsync(id);
            if (genero != null)
            {
                _context.Genero.Remove(genero);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeneroExists(int id)
        {
          return (_context.Genero?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task<bool> GeneroDuplicado(string Nombre)
        {
            var genero = await _context.Genero.Where(g => g.Nombre.ToUpper() == Nombre.ToUpper()).FirstOrDefaultAsync();

            if (genero == null)
            {
                return false;
            }

            return true;
        }
    }
}
