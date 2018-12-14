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
    public class HorarioParaTrocaEnfermeiroesController : Controller
    {
        private readonly HospitalDbContext _context;

        public HorarioParaTrocaEnfermeiroesController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: HorarioParaTrocaEnfermeiroes
        public async Task<IActionResult> Index()
        {
            return View(await _context.HorarioParaTrocaEnfermeiros
                .Include(h => h.HorarioEnfermeiro)
                .ToListAsync());
        }

        // GET: HorarioParaTrocaEnfermeiroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioParaTrocaEnfermeiro = await _context.HorarioParaTrocaEnfermeiros
                .Include(h => h.HorarioEnfermeiro)
                .FirstOrDefaultAsync(m => m.HorarioParaTrocaEnfermeiroId == id);
            if (horarioParaTrocaEnfermeiro == null)
            {
                return NotFound();
            }

            return View(horarioParaTrocaEnfermeiro);
        }

        // GET: HorarioParaTrocaEnfermeiroes/Create
        public IActionResult Create()
        {
            ViewData["HorarioEnfermeiroId"] = new SelectList(_context.HorariosEnfermeiro, "HorarioEnfermeiroId", "DataInicioTurno");
            return View();
        }

        // POST: HorarioParaTrocaEnfermeiroes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HorarioParaTrocaEnfermeiroId,HorarioEnfermeiroId")] HorarioParaTrocaEnfermeiro horarioParaTrocaEnfermeiro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horarioParaTrocaEnfermeiro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["HorarioEnfermeiroId"] = new SelectList(_context.HorariosEnfermeiro, "HorarioEnfermeiroId", "DataInicioTurno", horarioParaTrocaEnfermeiro.HorarioEnfermeiroId);
            return View(horarioParaTrocaEnfermeiro);
        }

        // GET: HorarioParaTrocaEnfermeiroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioParaTrocaEnfermeiro = await _context.HorarioParaTrocaEnfermeiros.FindAsync(id);
            if (horarioParaTrocaEnfermeiro == null)
            {
                return NotFound();
            }

            ViewData["HorarioEnfermeiroId"] = new SelectList(_context.HorariosEnfermeiro, "HorarioEnfermeiroId", "DataInicioTurno");
            return View(horarioParaTrocaEnfermeiro);
        }

        // POST: HorarioParaTrocaEnfermeiroes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HorarioParaTrocaEnfermeiroId,HorarioEnfermeiroId")] HorarioParaTrocaEnfermeiro horarioParaTrocaEnfermeiro)
        {
            if (id != horarioParaTrocaEnfermeiro.HorarioParaTrocaEnfermeiroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horarioParaTrocaEnfermeiro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorarioParaTrocaEnfermeiroExists(horarioParaTrocaEnfermeiro.HorarioParaTrocaEnfermeiroId))
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

            ViewData["HorarioEnfermeiroId"] = new SelectList(_context.HorariosEnfermeiro, "HorarioEnfermeiroId", "DataInicioTurno", horarioParaTrocaEnfermeiro.HorarioEnfermeiroId);
            return View(horarioParaTrocaEnfermeiro);
        }

        // GET: HorarioParaTrocaEnfermeiroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioParaTrocaEnfermeiro = await _context.HorarioParaTrocaEnfermeiros
                .Include(h => h.HorarioEnfermeiro)
                .FirstOrDefaultAsync(m => m.HorarioParaTrocaEnfermeiroId == id);
            if (horarioParaTrocaEnfermeiro == null)
            {
                return NotFound();
            }

            return View(horarioParaTrocaEnfermeiro);
        }

        // POST: HorarioParaTrocaEnfermeiroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horarioParaTrocaEnfermeiro = await _context.HorarioParaTrocaEnfermeiros.FindAsync(id);
            _context.HorarioParaTrocaEnfermeiros.Remove(horarioParaTrocaEnfermeiro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorarioParaTrocaEnfermeiroExists(int id)
        {
            return _context.HorarioParaTrocaEnfermeiros.Any(e => e.HorarioParaTrocaEnfermeiroId == id);
        }
    }
}
