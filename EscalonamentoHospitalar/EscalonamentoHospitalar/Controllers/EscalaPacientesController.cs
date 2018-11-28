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
    public class EscalaPacientesController : Controller
    {
        private readonly HospitalDbContext _context;

        public EscalaPacientesController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: EscalaPacientes
        public async Task<IActionResult> Index()
        {
            var hospitalDbContext = _context.EscalaPacientes.Include(e => e.Paciente).Include(e => e.Tratamento);
            return View(await hospitalDbContext.ToListAsync());
        }

        // GET: EscalaPacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escalaPaciente = await _context.EscalaPacientes
                .Include(e => e.Paciente)
                .Include(e => e.Tratamento)
                .FirstOrDefaultAsync(m => m.EscalaPacienteId == id);
            if (escalaPaciente == null)
            {
                return NotFound();
            }

            return View(escalaPaciente);
        }

        // GET: EscalaPacientes/Create
        public IActionResult Create()
        {
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Contacto");
            ViewData["TratamentoId"] = new SelectList(_context.Tratamentos, "TratamentoId", "DuracaoCiclo");
            return View();
        }

        // POST: EscalaPacientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EscalaPacienteId,TratamentoId,PacienteId")] EscalaPaciente escalaPaciente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(escalaPaciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Contacto", escalaPaciente.PacienteId);
            ViewData["TratamentoId"] = new SelectList(_context.Tratamentos, "TratamentoId", "DuracaoCiclo", escalaPaciente.TratamentoId);
            return View(escalaPaciente);
        }

        // GET: EscalaPacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escalaPaciente = await _context.EscalaPacientes.FindAsync(id);
            if (escalaPaciente == null)
            {
                return NotFound();
            }
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Contacto", escalaPaciente.PacienteId);
            ViewData["TratamentoId"] = new SelectList(_context.Tratamentos, "TratamentoId", "DuracaoCiclo", escalaPaciente.TratamentoId);
            return View(escalaPaciente);
        }

        // POST: EscalaPacientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EscalaPacienteId,TratamentoId,PacienteId")] EscalaPaciente escalaPaciente)
        {
            if (id != escalaPaciente.EscalaPacienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(escalaPaciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EscalaPacienteExists(escalaPaciente.EscalaPacienteId))
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
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Contacto", escalaPaciente.PacienteId);
            ViewData["TratamentoId"] = new SelectList(_context.Tratamentos, "TratamentoId", "DuracaoCiclo", escalaPaciente.TratamentoId);
            return View(escalaPaciente);
        }

        // GET: EscalaPacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escalaPaciente = await _context.EscalaPacientes
                .Include(e => e.Paciente)
                .Include(e => e.Tratamento)
                .FirstOrDefaultAsync(m => m.EscalaPacienteId == id);
            if (escalaPaciente == null)
            {
                return NotFound();
            }

            return View(escalaPaciente);
        }

        // POST: EscalaPacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var escalaPaciente = await _context.EscalaPacientes.FindAsync(id);
            _context.EscalaPacientes.Remove(escalaPaciente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EscalaPacienteExists(int id)
        {
            return _context.EscalaPacientes.Any(e => e.EscalaPacienteId == id);
        }
    }
}
