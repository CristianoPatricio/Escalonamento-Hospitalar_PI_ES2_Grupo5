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
    public class EspecialidadeMedicosController : Controller
    {
        private readonly HospitalDbContext _context;

        public EspecialidadeMedicosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: EspecialidadeMedicos
        public async Task<IActionResult> Index()
        {
            return View(await _context.EspecialidadeMedicos.ToListAsync());
        }

        // GET: EspecialidadeMedicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialidadeMedico = await _context.EspecialidadeMedicos
                .FirstOrDefaultAsync(m => m.EspecialidadeMedicoId == id);
            if (especialidadeMedico == null)
            {
                return NotFound();
            }

            return View(especialidadeMedico);
        }

        // GET: EspecialidadeMedicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EspecialidadeMedicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EspecialidadeMedicoId,NomeEspecialidade")] EspecialidadeMedico especialidadeMedico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(especialidadeMedico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(especialidadeMedico);
        }

        // GET: EspecialidadeMedicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialidadeMedico = await _context.EspecialidadeMedicos.FindAsync(id);
            if (especialidadeMedico == null)
            {
                return NotFound();
            }
            return View(especialidadeMedico);
        }

        // POST: EspecialidadeMedicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EspecialidadeMedicoId,NomeEspecialidade")] EspecialidadeMedico especialidadeMedico)
        {
            if (id != especialidadeMedico.EspecialidadeMedicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(especialidadeMedico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspecialidadeMedicoExists(especialidadeMedico.EspecialidadeMedicoId))
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
            return View(especialidadeMedico);
        }

        // GET: EspecialidadeMedicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialidadeMedico = await _context.EspecialidadeMedicos
                .FirstOrDefaultAsync(m => m.EspecialidadeMedicoId == id);
            if (especialidadeMedico == null)
            {
                return NotFound();
            }

            return View(especialidadeMedico);
        }

        // POST: EspecialidadeMedicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var especialidadeMedico = await _context.EspecialidadeMedicos.FindAsync(id);
            _context.EspecialidadeMedicos.Remove(especialidadeMedico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EspecialidadeMedicoExists(int id)
        {
            return _context.EspecialidadeMedicos.Any(e => e.EspecialidadeMedicoId == id);
        }
    }
}