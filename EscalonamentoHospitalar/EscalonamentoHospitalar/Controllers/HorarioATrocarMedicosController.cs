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
    public class HorarioATrocarMedicosController : Controller
    {
        private readonly HospitalDbContext _context;

        public HorarioATrocarMedicosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: HorarioATrocarMedicos
        public async Task<IActionResult> Index()
        {
            var hospitalDbContext = _context.HorarioATrocarMedico.Include(h => h.HorarioMedico);
            return View(await hospitalDbContext.ToListAsync());
        }

        // GET: HorarioATrocarMedicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioATrocarMedico = await _context.HorarioATrocarMedico
                .Include(h => h.HorarioMedico)
                .FirstOrDefaultAsync(m => m.HorarioATrocarMedicoId == id);
            if (horarioATrocarMedico == null)
            {
                return NotFound();
            }

            return View(horarioATrocarMedico);
        }

        // GET: HorarioATrocarMedicos/Create
        public IActionResult Create()
        {
            ViewData["HorarioMedicoId"] = new SelectList(_context.HorariosMedicos, "HorarioMedicoId", "HorarioMedicoId");
            return View();
        }

        // POST: HorarioATrocarMedicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HorarioATrocarMedicoId,HorarioMedicoId")] HorarioATrocarMedico horarioATrocarMedico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horarioATrocarMedico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HorarioMedicoId"] = new SelectList(_context.HorariosMedicos, "HorarioMedicoId", "HorarioMedicoId", horarioATrocarMedico.HorarioMedicoId);
            return View(horarioATrocarMedico);
        }

        // GET: HorarioATrocarMedicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioATrocarMedico = await _context.HorarioATrocarMedico.FindAsync(id);
            if (horarioATrocarMedico == null)
            {
                return NotFound();
            }
            ViewData["HorarioMedicoId"] = new SelectList(_context.HorariosMedicos, "HorarioMedicoId", "HorarioMedicoId", horarioATrocarMedico.HorarioMedicoId);
            return View(horarioATrocarMedico);
        }

        // POST: HorarioATrocarMedicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HorarioATrocarMedicoId,HorarioMedicoId")] HorarioATrocarMedico horarioATrocarMedico)
        {
            if (id != horarioATrocarMedico.HorarioATrocarMedicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horarioATrocarMedico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorarioATrocarMedicoExists(horarioATrocarMedico.HorarioATrocarMedicoId))
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
            ViewData["HorarioMedicoId"] = new SelectList(_context.HorariosMedicos, "HorarioMedicoId", "HorarioMedicoId", horarioATrocarMedico.HorarioMedicoId);
            return View(horarioATrocarMedico);
        }

        // GET: HorarioATrocarMedicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioATrocarMedico = await _context.HorarioATrocarMedico
                .Include(h => h.HorarioMedico)
                .FirstOrDefaultAsync(m => m.HorarioATrocarMedicoId == id);
            if (horarioATrocarMedico == null)
            {
                return NotFound();
            }

            return View(horarioATrocarMedico);
        }

        // POST: HorarioATrocarMedicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horarioATrocarMedico = await _context.HorarioATrocarMedico.FindAsync(id);
            _context.HorarioATrocarMedico.Remove(horarioATrocarMedico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorarioATrocarMedicoExists(int id)
        {
            return _context.HorarioATrocarMedico.Any(e => e.HorarioATrocarMedicoId == id);
        }
    }
}
