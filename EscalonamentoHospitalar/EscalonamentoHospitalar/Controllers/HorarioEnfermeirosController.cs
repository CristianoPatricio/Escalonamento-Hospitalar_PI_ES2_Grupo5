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
    public class HorarioEnfermeirosController : Controller
    {
        private readonly HospitalDbContext _context;

        public HorarioEnfermeirosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: HorarioEnfermeiros
        public async Task<IActionResult> Index()
        {
            var hospitalDbContext = _context.HorariosEnfermeiro.Include(h => h.Enfermeiro).Include(h => h.Turno);
            return View(await hospitalDbContext.ToListAsync());
        }

        // GET: HorarioEnfermeiros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioEnfermeiro = await _context.HorariosEnfermeiro
                .Include(h => h.Enfermeiro)
                .Include(h => h.Turno)
                .FirstOrDefaultAsync(m => m.HorarioEnfermeiroId == id);
            if (horarioEnfermeiro == null)
            {
                return NotFound();
            }

            return View(horarioEnfermeiro);
        }

        // GET: HorarioEnfermeiros/Create
        public IActionResult Create()
        {
            ViewData["EnfermeiroId"] = new SelectList(_context.Enfermeiros, "EnfermeiroId", "CC");
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "TurnoId", "TurnoId");
            return View();
        }

        // POST: HorarioEnfermeiros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HorarioEnfermeiroId,DataInicioTurno,Duracao,DataFimTurno,TurnoId,EnfermeiroId")] HorarioEnfermeiro horarioEnfermeiro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horarioEnfermeiro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnfermeiroId"] = new SelectList(_context.Enfermeiros, "EnfermeiroId", "CC", horarioEnfermeiro.EnfermeiroId);
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "TurnoId", "TurnoId", horarioEnfermeiro.TurnoId);
            return View(horarioEnfermeiro);
        }

        // GET: HorarioEnfermeiros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioEnfermeiro = await _context.HorariosEnfermeiro.FindAsync(id);
            if (horarioEnfermeiro == null)
            {
                return NotFound();
            }
            ViewData["EnfermeiroId"] = new SelectList(_context.Enfermeiros, "EnfermeiroId", "CC", horarioEnfermeiro.EnfermeiroId);
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "TurnoId", "TurnoId", horarioEnfermeiro.TurnoId);
            return View(horarioEnfermeiro);
        }

        // POST: HorarioEnfermeiros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HorarioEnfermeiroId,DataInicioTurno,Duracao,DataFimTurno,TurnoId,EnfermeiroId")] HorarioEnfermeiro horarioEnfermeiro)
        {
            if (id != horarioEnfermeiro.HorarioEnfermeiroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horarioEnfermeiro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorarioEnfermeiroExists(horarioEnfermeiro.HorarioEnfermeiroId))
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
            ViewData["EnfermeiroId"] = new SelectList(_context.Enfermeiros, "EnfermeiroId", "CC", horarioEnfermeiro.EnfermeiroId);
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "TurnoId", "TurnoId", horarioEnfermeiro.TurnoId);
            return View(horarioEnfermeiro);
        }

        // GET: HorarioEnfermeiros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioEnfermeiro = await _context.HorariosEnfermeiro
                .Include(h => h.Enfermeiro)
                .Include(h => h.Turno)
                .FirstOrDefaultAsync(m => m.HorarioEnfermeiroId == id);
            if (horarioEnfermeiro == null)
            {
                return NotFound();
            }

            return View(horarioEnfermeiro);
        }

        // POST: HorarioEnfermeiros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horarioEnfermeiro = await _context.HorariosEnfermeiro.FindAsync(id);
            _context.HorariosEnfermeiro.Remove(horarioEnfermeiro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorarioEnfermeiroExists(int id)
        {
            return _context.HorariosEnfermeiro.Any(e => e.HorarioEnfermeiroId == id);
        }
    }
}
