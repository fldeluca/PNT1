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
    public class ReservasController : Controller
    {
        private readonly DbContext _context;
        private const double _precioDefault = 150;

        public ReservasController(DbContext context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var dbContext = _context.Reserva.Include(r => r.Cliente).Include(r => r.Funcion);
            return View(await dbContext.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reserva == null)
            {
                return RedirectToAction("VistaError", "Home");
            }

            var reserva = await _context.Reserva
                .Include(r => r.Cliente)
                .Include(r => r.Funcion)
                .FirstOrDefaultAsync(m => m.ReservaId == id);
            if (reserva == null)
            {
                return RedirectToAction("VistaError", "Home");
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "Apellido");
            ViewData["FuncionId"] = new SelectList(_context.Funcion, "FuncionId", "FuncionId");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservaId,FechaReserva,Precio,CantidadAsientos,ReservaConfirmada,ClienteId,FuncionId")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {

                Funcion? funcion = await _context.Funcion.FindAsync(reserva.FuncionId);
                

                if(funcion != null)
                {
                    reserva.Funcion = funcion;
                    if (funcion.AsientosDisponibles - reserva.CantidadAsientos >= 0)
                    {
                        reserva.Precio = reserva.CantidadAsientos * _precioDefault;
                        funcion.AsientosDisponibles -= reserva.CantidadAsientos;
                        reserva.FechaReserva = DateTime.Now;
                        reserva.ReservaConfirmada = true;
                        _context.Add(reserva);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    } else
                    {
                        return RedirectToAction("VistaError", "Home");
                    }

                    
                } else
                {
                    return RedirectToAction("VistaError", "Home");
                }
                
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "Apellido", reserva.ClienteId);
            ViewData["FuncionId"] = new SelectList(_context.Funcion, "FuncionId", "FuncionId", reserva.FuncionId);
            return View(reserva);
        }

        

     

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reserva == null)
            {
                return RedirectToAction("VistaError", "Home");
            }

            var reserva = await _context.Reserva
                .Include(r => r.Cliente)
                .Include(r => r.Funcion)
                .FirstOrDefaultAsync(m => m.ReservaId == id);
            if (reserva == null)
            {
                return RedirectToAction("VistaError", "Home");
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reserva == null)
            {
                return Problem("Entity set 'DbContext.Reserva'  is null.");
            }
            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva != null)
            {
                Funcion? funcion = await _context.Funcion.FindAsync(reserva.FuncionId);
                if (funcion != null)
                {
                    funcion.AsientosDisponibles += reserva.CantidadAsientos;
                    _context.Reserva.Remove(reserva);
                    await _context.SaveChangesAsync();
                } else
                {
                    return RedirectToAction("VistaError", "Home");
                }
                
            }
            
            
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
          return (_context.Reserva?.Any(e => e.ReservaId == id)).GetValueOrDefault();
        }
    }
}
