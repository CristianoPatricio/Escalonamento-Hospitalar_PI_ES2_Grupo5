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
    public class HistoricoTratamentosController : Controller
    {
        private readonly HospitalDbContext _context;

        public HistoricoTratamentosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: HistoricoTratamentos
        public async Task<IActionResult> Index()
        {
            var hospitalDbContext = _context.HistoricoTratamento.Include(h => h.Paciente).Include(h => h.Tratamento);
            return View(await hospitalDbContext.ToListAsync());
        }

        // GET: HistoricoTratamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoTratamento = await _context.HistoricoTratamento
                .Include(h => h.Paciente)
                .Include(h => h.Tratamento)
                .FirstOrDefaultAsync(m => m.HistoricoTratamentoId == id);
            if (historicoTratamento == null)
            {
                return NotFound();
            }

            return View(historicoTratamento);
        }

        // GET: HistoricoTratamentos/Create
        public IActionResult Create()
        {
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "CC");
            ViewData["TratamentoId"] = new SelectList(_context.Tratamentos, "TratamentoId", "TratamentoId");
            return View();
        }

        
        // POST: HistoricoTratamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HistoricoTratamentoId,TratamentoId,PacienteId,DataInicio,DataFim")] HistoricoTratamento historicoTratamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historicoTratamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "CC", historicoTratamento.PacienteId);
            ViewData["TratamentoId"] = new SelectList(_context.Tratamentos, "TratamentoId", "TratamentoId", historicoTratamento.TratamentoId);
            return View(historicoTratamento);
        }

        // GET: HistoricoTratamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoTratamento = await _context.HistoricoTratamento.FindAsync(id);
            if (historicoTratamento == null)
            {
                return NotFound();
            }
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "CC", historicoTratamento.PacienteId);
            ViewData["TratamentoId"] = new SelectList(_context.Tratamentos, "TratamentoId", "TratamentoId", historicoTratamento.TratamentoId);
            return View(historicoTratamento);
        }

        // POST: HistoricoTratamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HistoricoTratamentoId,TratamentoId,PacienteId,DataInicio,DataFim")] HistoricoTratamento historicoTratamento)
        {
            if (id != historicoTratamento.HistoricoTratamentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historicoTratamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoricoTratamentoExists(historicoTratamento.HistoricoTratamentoId))
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
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "CC", historicoTratamento.PacienteId);
            ViewData["TratamentoId"] = new SelectList(_context.Tratamentos, "TratamentoId", "TratamentoId", historicoTratamento.TratamentoId);
            return View(historicoTratamento);
        }

        // GET: HistoricoTratamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoTratamento = await _context.HistoricoTratamento
                .Include(h => h.Paciente)
                .Include(h => h.Tratamento)
                .FirstOrDefaultAsync(m => m.HistoricoTratamentoId == id);
            if (historicoTratamento == null)
            {
                return NotFound();
            }

            return View(historicoTratamento);
        }

        // POST: HistoricoTratamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historicoTratamento = await _context.HistoricoTratamento.FindAsync(id);
            _context.HistoricoTratamento.Remove(historicoTratamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoricoTratamentoExists(int id)
        {
            return _context.HistoricoTratamento.Any(e => e.HistoricoTratamentoId == id);
        }
    }
}
