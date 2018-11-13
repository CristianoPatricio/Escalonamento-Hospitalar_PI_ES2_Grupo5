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
    public class EspecialidadeEnfermeirosController : Controller
    {
        private readonly HospitalDbContext _context;

        public EspecialidadeEnfermeirosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: EspecialidadeEnfermeiros
        public async Task<IActionResult> Index()
        {
            return View(await _context.EspecialidadesEnfermeiros.ToListAsync());
        }

        // GET: EspecialidadeEnfermeiros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialidadeEnfermeiro = await _context.EspecialidadesEnfermeiros
                .FirstOrDefaultAsync(m => m.EspecialidadeEnfermeiroId == id);
            if (especialidadeEnfermeiro == null)
            {
                return NotFound();
            }

            return View(especialidadeEnfermeiro);
        }

        // GET: EspecialidadeEnfermeiros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EspecialidadeEnfermeiros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EspecialidadeEnfermeiroId,Especialidade")] EspecialidadeEnfermeiro especialidadeEnfermeiro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(especialidadeEnfermeiro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(especialidadeEnfermeiro);
        }

        // GET: EspecialidadeEnfermeiros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialidadeEnfermeiro = await _context.EspecialidadesEnfermeiros.FindAsync(id);
            if (especialidadeEnfermeiro == null)
            {
                return NotFound();
            }
            return View(especialidadeEnfermeiro);
        }

        // POST: EspecialidadeEnfermeiros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EspecialidadeEnfermeiroId,Especialidade")] EspecialidadeEnfermeiro especialidadeEnfermeiro)
        {
            if (id != especialidadeEnfermeiro.EspecialidadeEnfermeiroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(especialidadeEnfermeiro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspecialidadeEnfermeiroExists(especialidadeEnfermeiro.EspecialidadeEnfermeiroId))
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
            return View(especialidadeEnfermeiro);
        }

        // GET: EspecialidadeEnfermeiros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialidadeEnfermeiro = await _context.EspecialidadesEnfermeiros
                .FirstOrDefaultAsync(m => m.EspecialidadeEnfermeiroId == id);
            if (especialidadeEnfermeiro == null)
            {
                return NotFound();
            }

            return View(especialidadeEnfermeiro);
        }

        // POST: EspecialidadeEnfermeiros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var especialidadeEnfermeiro = await _context.EspecialidadesEnfermeiros.FindAsync(id);
            _context.EspecialidadesEnfermeiros.Remove(especialidadeEnfermeiro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EspecialidadeEnfermeiroExists(int id)
        {
            return _context.EspecialidadesEnfermeiros.Any(e => e.EspecialidadeEnfermeiroId == id);
        }
    }
}
