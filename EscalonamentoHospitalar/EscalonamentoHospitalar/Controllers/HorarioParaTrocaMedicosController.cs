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
    public class HorarioParaTrocaMedicosController : Controller
    {
        private readonly HospitalDbContext _context;

        public HorarioParaTrocaMedicosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: HorarioParaTrocaMedicos
        public async Task<IActionResult> Index()
        {
            var hospitalDbContext = _context.HorarioParaTrocaMedico.Include(h => h.HorarioMedico);
            return View(await hospitalDbContext.ToListAsync());
        }

        // GET: HorarioParaTrocaMedicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioParaTrocaMedico = await _context.HorarioParaTrocaMedico
                .Include(h => h.HorarioMedico)
                .FirstOrDefaultAsync(m => m.HorarioParaTrocaMedicoId == id);
            if (horarioParaTrocaMedico == null)
            {
                return NotFound();
            }

            return View(horarioParaTrocaMedico);
        }

        // GET: HorarioParaTrocaMedicos/Create
        public IActionResult Create()
        {
            ViewData["HorarioMedicoId"] = new SelectList(_context.HorariosMedicos, "HorarioMedicoId", "HorarioMedicoId");
            return View();
        }

        // POST: HorarioParaTrocaMedicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HorarioParaTrocaMedicoId,HorarioMedicoId")] HorarioParaTrocaMedico horarioParaTrocaMedico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horarioParaTrocaMedico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HorarioMedicoId"] = new SelectList(_context.HorariosMedicos, "HorarioMedicoId", "HorarioMedicoId", horarioParaTrocaMedico.HorarioMedicoId);
            return View(horarioParaTrocaMedico);
        }

        // GET: HorarioParaTrocaMedicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioParaTrocaMedico = await _context.HorarioParaTrocaMedico.FindAsync(id);
            if (horarioParaTrocaMedico == null)
            {
                return NotFound();
            }
            ViewData["HorarioMedicoId"] = new SelectList(_context.HorariosMedicos, "HorarioMedicoId", "HorarioMedicoId", horarioParaTrocaMedico.HorarioMedicoId);
            return View(horarioParaTrocaMedico);
        }

        // POST: HorarioParaTrocaMedicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HorarioParaTrocaMedicoId,HorarioMedicoId")] HorarioParaTrocaMedico horarioParaTrocaMedico)
        {
            if (id != horarioParaTrocaMedico.HorarioParaTrocaMedicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horarioParaTrocaMedico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorarioParaTrocaMedicoExists(horarioParaTrocaMedico.HorarioParaTrocaMedicoId))
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
            ViewData["HorarioMedicoId"] = new SelectList(_context.HorariosMedicos, "HorarioMedicoId", "HorarioMedicoId", horarioParaTrocaMedico.HorarioMedicoId);
            return View(horarioParaTrocaMedico);
        }

        // GET: HorarioParaTrocaMedicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioParaTrocaMedico = await _context.HorarioParaTrocaMedico
                .Include(h => h.HorarioMedico)
                .FirstOrDefaultAsync(m => m.HorarioParaTrocaMedicoId == id);
            if (horarioParaTrocaMedico == null)
            {
                return NotFound();
            }

            return View(horarioParaTrocaMedico);
        }

        // POST: HorarioParaTrocaMedicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horarioParaTrocaMedico = await _context.HorarioParaTrocaMedico.FindAsync(id);
            _context.HorarioParaTrocaMedico.Remove(horarioParaTrocaMedico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorarioParaTrocaMedicoExists(int id)
        {
            return _context.HorarioParaTrocaMedico.Any(e => e.HorarioParaTrocaMedicoId == id);
        }
    }
}
