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
    public class TratamentosController : Controller
    {
        private readonly HospitalDbContext _context;

        public TratamentosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: Tratamentos
        public async Task<IActionResult> Index()
        {
            var hospitalDbContext = _context.Tratamento.Include(t => t.Grau).Include(t => t.Medico).Include(t => t.Paciente).Include(t => t.Patologia);
            return View(await hospitalDbContext.ToListAsync());
        }

        // GET: Tratamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamento = await _context.Tratamento
                .Include(t => t.Grau)
                .Include(t => t.Medico)
                .Include(t => t.Paciente)
                .Include(t => t.Patologia)
                .FirstOrDefaultAsync(m => m.TratamentoId == id);
            if (tratamento == null)
            {
                return NotFound();
            }

            return View(tratamento);
        }

        // GET: Tratamentos/Create
        public IActionResult Create()
        {
            ViewData["GrauId"] = new SelectList(_context.Grau, "GrauId", "GrauId");
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "MedicoId");
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "CC");
            ViewData["PatologiaId"] = new SelectList(_context.Patologia, "PatologiaId", "PatologiaId");
            return View();
        }

        // POST: Tratamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TratamentoId,PatologiaId,PacienteId,GrauId,DataInicio,DataFim,DuracaoCiclo,Regime,Decorrer,Concluido,MedicoId")] Tratamento tratamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tratamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GrauId"] = new SelectList(_context.Grau, "GrauId", "GrauId", tratamento.GrauId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "MedicoId", tratamento.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "CC", tratamento.PacienteId);
            ViewData["PatologiaId"] = new SelectList(_context.Patologia, "PatologiaId", "PatologiaId", tratamento.PatologiaId);
            return View(tratamento);
        }

        // GET: Tratamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamento = await _context.Tratamento.FindAsync(id);
            if (tratamento == null)
            {
                return NotFound();
            }
            ViewData["GrauId"] = new SelectList(_context.Grau, "GrauId", "GrauId", tratamento.GrauId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "MedicoId", tratamento.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "CC", tratamento.PacienteId);
            ViewData["PatologiaId"] = new SelectList(_context.Patologia, "PatologiaId", "PatologiaId", tratamento.PatologiaId);
            return View(tratamento);
        }

        // POST: Tratamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TratamentoId,PatologiaId,PacienteId,GrauId,DataInicio,DataFim,DuracaoCiclo,Regime,Decorrer,Concluido,MedicoId")] Tratamento tratamento)
        {
            if (id != tratamento.TratamentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tratamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TratamentoExists(tratamento.TratamentoId))
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
            ViewData["GrauId"] = new SelectList(_context.Grau, "GrauId", "GrauId", tratamento.GrauId);
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "MedicoId", tratamento.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "CC", tratamento.PacienteId);
            ViewData["PatologiaId"] = new SelectList(_context.Patologia, "PatologiaId", "PatologiaId", tratamento.PatologiaId);
            return View(tratamento);
        }

        // GET: Tratamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamento = await _context.Tratamento
                .Include(t => t.Grau)
                .Include(t => t.Medico)
                .Include(t => t.Paciente)
                .Include(t => t.Patologia)
                .FirstOrDefaultAsync(m => m.TratamentoId == id);
            if (tratamento == null)
            {
                return NotFound();
            }

            return View(tratamento);
        }

        // POST: Tratamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tratamento = await _context.Tratamento.FindAsync(id);
            _context.Tratamento.Remove(tratamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TratamentoExists(int id)
        {
            return _context.Tratamento.Any(e => e.TratamentoId == id);
        }
    }
}
