using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EscalonamentoHospitalar.Models;

namespace EscalonamentoHospitalar.Controllers
{
    public class EscalaEnfermeirosController : Controller
    {
        private readonly HospitalDbContext _context;

        public EscalaEnfermeirosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: EscalaEnfermeiros
        public async Task<IActionResult> Index()
        {
            var hospitalDbContext = _context.EscalaEnfermeiros.Include(e => e.Enfermeiro).Include(e => e.Turno);
            return View(await hospitalDbContext.ToListAsync());
        }

        // GET: EscalaEnfermeiros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escalaEnfermeiro = await _context.EscalaEnfermeiros
                .Include(e => e.Enfermeiro)
               
                .Include(e => e.Turno)
                .FirstOrDefaultAsync(m => m.EscalaEnfermeiroId == id);
            if (escalaEnfermeiro == null)
            {
                return NotFound();
            }

            return View(escalaEnfermeiro);
        }

        // GET: EscalaEnfermeiros/Create
        public IActionResult Create()
        {
            ViewData["EnfermeiroId"] = new SelectList(_context.Enfermeiros, "EnfermeiroId", "CC");
           
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "TurnoId", "TurnoId");
            return View();
        }

        // POST: EscalaEnfermeiros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EscalaEnfermeiroId,TurnoId,EnfermeiroId")] EscalaEnfermeiro escalaEnfermeiro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(escalaEnfermeiro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnfermeiroId"] = new SelectList(_context.Enfermeiros, "EnfermeiroId", "CC", escalaEnfermeiro.EnfermeiroId);
            
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "TurnoId", "TurnoId", escalaEnfermeiro.TurnoId);
            return View(escalaEnfermeiro);
        }

        // GET: EscalaEnfermeiros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escalaEnfermeiro = await _context.EscalaEnfermeiros.FindAsync(id);
            if (escalaEnfermeiro == null)
            {
                return NotFound();
            }
            ViewData["EnfermeiroId"] = new SelectList(_context.Enfermeiros, "EnfermeiroId", "CC", escalaEnfermeiro.EnfermeiroId);
            
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "TurnoId", "TurnoId", escalaEnfermeiro.TurnoId);
            return View(escalaEnfermeiro);
        }

        // POST: EscalaEnfermeiros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EscalaEnfermeiroId,TurnoId,EnfermeiroId")] EscalaEnfermeiro escalaEnfermeiro)
        {
            if (id != escalaEnfermeiro.EscalaEnfermeiroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(escalaEnfermeiro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EscalaEnfermeiroExists(escalaEnfermeiro.EscalaEnfermeiroId))
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
            ViewData["EnfermeiroId"] = new SelectList(_context.Enfermeiros, "EnfermeiroId", "CC", escalaEnfermeiro.EnfermeiroId);
            
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "TurnoId", "TurnoId", escalaEnfermeiro.TurnoId);
            return View(escalaEnfermeiro);
        }

        // GET: EscalaEnfermeiros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escalaEnfermeiro = await _context.EscalaEnfermeiros
                .Include(e => e.Enfermeiro)
               
                .Include(e => e.Turno)
                .FirstOrDefaultAsync(m => m.EscalaEnfermeiroId == id);
            if (escalaEnfermeiro == null)
            {
                return NotFound();
            }

            return View(escalaEnfermeiro);
        }

        // POST: EscalaEnfermeiros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var escalaEnfermeiro = await _context.EscalaEnfermeiros.FindAsync(id);
            _context.EscalaEnfermeiros.Remove(escalaEnfermeiro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EscalaEnfermeiroExists(int id)
        {
            return _context.EscalaEnfermeiros.Any(e => e.EscalaEnfermeiroId == id);
        }
    }
}
