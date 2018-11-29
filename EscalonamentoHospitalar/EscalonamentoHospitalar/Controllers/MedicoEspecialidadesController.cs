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
    public class MedicoEspecialidadesController : Controller
    {
        private readonly HospitalDbContext _context;

        public MedicoEspecialidadesController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: MedicoEspecialidades
        public async Task<IActionResult> Index()
        {
            var hospitalDbContext = _context.MedicoEspecialidades.Include(m => m.EspecialidadeMedico).Include(m => m.Medico);
            return View(await hospitalDbContext.ToListAsync());
        }

        // GET: MedicoEspecialidades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicoEspecialidade = await _context.MedicoEspecialidades
                .Include(m => m.EspecialidadeMedico)
                .Include(m => m.Medico)
                .FirstOrDefaultAsync(m => m.MedicoId == id);
            if (medicoEspecialidade == null)
            {
                return NotFound();
            }

            return View(medicoEspecialidade);
        }

        // GET: MedicoEspecialidades/Create
        public IActionResult Create()
        {
            ViewData["EspecialidadeMedicoId"] = new SelectList(_context.EspecialidadeMedicos, "EspecialidadeMedicoId", "NomeEspecialidade");
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "Nome");
            return View();
        }

        // POST: MedicoEspecialidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicoId, NomeEspecialidade, Data_Registo")] MedicoEspecialidade medicoEspecialidade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicoEspecialidade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EspecialidadeMedicoId"] = new SelectList(_context.EspecialidadeMedicos, "EspecialidadeMedicoId", "NomeEspecialidade", medicoEspecialidade.EspecialidadeMedicoId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "Nome", medicoEspecialidade.MedicoId);
            return View(medicoEspecialidade);
        }

        // GET: MedicoEspecialidades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicoEspecialidade = await _context.MedicoEspecialidades.FindAsync(id);
            if (medicoEspecialidade == null)
            {
                return NotFound();
            }
            ViewData["EspecialidadeMedicoId"] = new SelectList(_context.EspecialidadeMedicos, "EspecialidadeMedicoId", "NomeEspecialidade", medicoEspecialidade.EspecialidadeMedicoId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "Nome", medicoEspecialidade.MedicoId);
            return View(medicoEspecialidade);
        }

        // POST: MedicoEspecialidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicoId,NomeEspecialidade, Data_Registo")] MedicoEspecialidade medicoEspecialidade)
        {
            if (id != medicoEspecialidade.MedicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicoEspecialidade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoEspecialidadeExists(medicoEspecialidade.MedicoId))
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
            ViewData["EspecialidadeMedicoId"] = new SelectList(_context.EspecialidadeMedicos, "EspecialidadeMedicoId", "NomeEspecialidade", medicoEspecialidade.EspecialidadeMedicoId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "Nome", medicoEspecialidade.MedicoId);
            return View(medicoEspecialidade);
        }

        // GET: MedicoEspecialidades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicoEspecialidade = await _context.MedicoEspecialidades
                .Include(m => m.EspecialidadeMedico)
                .Include(m => m.Medico)
                .FirstOrDefaultAsync(m => m.MedicoId == id);
            if (medicoEspecialidade == null)
            {
                return NotFound();
            }

            return View(medicoEspecialidade);
        }

        // POST: MedicoEspecialidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicoEspecialidade = await _context.MedicoEspecialidades.FindAsync(id);
            _context.MedicoEspecialidades.Remove(medicoEspecialidade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoEspecialidadeExists(int id)
        {
            return _context.MedicoEspecialidades.Any(e => e.MedicoId == id);
        }
    }
}