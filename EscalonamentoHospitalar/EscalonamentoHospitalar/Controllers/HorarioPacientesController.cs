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
    public class HorarioPacientesController : Controller
    {
        private readonly HospitalDbContext _context;

        public HorarioPacientesController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: HorarioPacientes
        public async Task<IActionResult> Index()
        {
            var hospitalDbContext = _context.HorariosPaciente.Include(h => h.Paciente).Include(h => h.Tratamento);
            return View(await hospitalDbContext.ToListAsync());
        }

        // GET: HorarioPacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioPaciente = await _context.HorariosPaciente
                .Include(h => h.Paciente)
                .Include(h => h.Tratamento)
                .FirstOrDefaultAsync(m => m.HorarioPacienteId == id);
            if (horarioPaciente == null)
            {
                return NotFound();
            }

            return View(horarioPaciente);
        }

        // GET: HorarioPacientes/Create
        public IActionResult Create()
        {
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Nome");
            ViewData["TratamentoId"] = new SelectList(_context.Tratamentos, "TratamentoId", "PatologiaId");
            return View();
        }

        // POST: HorarioPacientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HorarioPacienteId,DataInicio,Duracao,DataFim,PacienteId,TratamentoId")] HorarioPaciente horarioPaciente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horarioPaciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "CC", horarioPaciente.PacienteId);
            ViewData["TratamentoId"] = new SelectList(_context.Tratamentos, "TratamentoId", "TratamentoId", horarioPaciente.TratamentoId);
            return View(horarioPaciente);
        }

        // GET: HorarioPacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioPaciente = await _context.HorariosPaciente.FindAsync(id);
            if (horarioPaciente == null)
            {
                return NotFound();
            }
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "CC", horarioPaciente.PacienteId);
            ViewData["TratamentoId"] = new SelectList(_context.Tratamentos, "TratamentoId", "TratamentoId", horarioPaciente.TratamentoId);
            return View(horarioPaciente);
        }

        // POST: HorarioPacientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HorarioPacienteId,DataInicio,Duracao,DataFim,PacienteId,TratamentoId")] HorarioPaciente horarioPaciente)
        {
            if (id != horarioPaciente.HorarioPacienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horarioPaciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorarioPacienteExists(horarioPaciente.HorarioPacienteId))
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
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "CC", horarioPaciente.PacienteId);
            ViewData["TratamentoId"] = new SelectList(_context.Tratamentos, "TratamentoId", "TratamentoId", horarioPaciente.TratamentoId);
            return View(horarioPaciente);
        }

        // GET: HorarioPacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioPaciente = await _context.HorariosPaciente
                .Include(h => h.Paciente)
                .Include(h => h.Tratamento)
                .FirstOrDefaultAsync(m => m.HorarioPacienteId == id);
            if (horarioPaciente == null)
            {
                return NotFound();
            }

            return View(horarioPaciente);
        }

        // POST: HorarioPacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horarioPaciente = await _context.HorariosPaciente.FindAsync(id);
            _context.HorariosPaciente.Remove(horarioPaciente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorarioPacienteExists(int id)
        {
            return _context.HorariosPaciente.Any(e => e.HorarioPacienteId == id);
        }
    }
}
