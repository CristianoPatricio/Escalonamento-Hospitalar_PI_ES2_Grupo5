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
    public class TurnosController : Controller
    {
        private readonly HospitalDbContext _context;

        public TurnosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: Turnos
        public async Task<IActionResult> Index()
        {
EscalonamentoHospitalar/EscalonamentoHospitalar/Controllers/MedicosController.cs
            var hospitalDbContext = _context.Medicos.Include(m => m.EspecialidadeMedico);
            return View(await hospitalDbContext.ToListAsync());

            return View(await _context.Turnos.ToListAsync());
EscalonamentoHospitalar/EscalonamentoHospitalar/Controllers/TurnosController.cs
        }

        // GET: Turnos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

EscalonamentoHospitalar/EscalonamentoHospitalar/Controllers/MedicosController.cs
            var medico = await _context.Medicos
                .Include(m => m.EspecialidadeMedico)
                .FirstOrDefaultAsync(m => m.MedicoId == id);
            if (medico == null)

            var turno = await _context.Turnos
                .FirstOrDefaultAsync(m => m.TurnoId == id);
            if (turno == null)
EscalonamentoHospitalar/EscalonamentoHospitalar/Controllers/TurnosController.cs
            {
                return NotFound();
            }

            return View(turno);
        }

        // GET: Turnos/Create
        public IActionResult Create()
        {
            ViewData["EspecialidadeMedicoId"] = new SelectList(_context.EspecialidadeMedicos, "EspecialidadeMedicoId", "NomeEspecialidade");
            return View();
        }

        // POST: Turnos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
EscalonamentoHospitalar/EscalonamentoHospitalar/Controllers/MedicosController.cs
        public async Task<IActionResult> Create([Bind("MedicoId,NumeroMecanografico,Nome,Email,Contacto,CC,Data_Nascimento,EspecialidadeMedicoId,Data_Inicio_Servico")] Medico medico)

     
        public async Task<IActionResult> Create([Bind("TurnoId,Nome")] Turno turno)
EscalonamentoHospitalar/EscalonamentoHospitalar/Controllers/TurnosController.cs
        {
            if (ModelState.IsValid)
            {
                _context.Add(turno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
EscalonamentoHospitalar/EscalonamentoHospitalar/Controllers/MedicosController.cs
            ViewData["EspecialidadeMedicoId"] = new SelectList(_context.EspecialidadeMedicos, "EspecialidadeMedicoId", "NomeEspecialidade", medico.EspecialidadeMedicoId);
            return View(medico);

            return View(turno);
EscalonamentoHospitalar/EscalonamentoHospitalar/Controllers/TurnosController.cs
        }

        // GET: Turnos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }
EscalonamentoHospitalar/EscalonamentoHospitalar/Controllers/MedicosController.cs
            ViewData["EspecialidadeMedicoId"] = new SelectList(_context.EspecialidadeMedicos, "EspecialidadeMedicoId", "NomeEspecialidade", medico.EspecialidadeMedicoId);
            return View(medico);

            return View(turno);
EscalonamentoHospitalar/EscalonamentoHospitalar/Controllers/TurnosController.cs
        }

        // POST: Turnos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
EscalonamentoHospitalar/EscalonamentoHospitalar/Controllers/MedicosController.cs
        public async Task<IActionResult> Edit(int id, [Bind("MedicoId,NumeroMecanografico,Nome,Email,Contacto,CC,Data_Nascimento,EspecialidadeMedicoId,Data_Inicio_Servico")] Medico medico)

        public async Task<IActionResult> Edit(int id, [Bind("TurnoId,Nome")] Turno turno)
EscalonamentoHospitalar/EscalonamentoHospitalar/Controllers/TurnosController.cs
        {
            if (id != turno.TurnoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoExists(turno.TurnoId))
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
EscalonamentoHospitalar/EscalonamentoHospitalar/Controllers/MedicosController.cs
            ViewData["EspecialidadeMedicoId"] = new SelectList(_context.EspecialidadeMedicos, "EspecialidadeMedicoId", "NomeEspecialidade", medico.EspecialidadeMedicoId);
            return View(medico);

            return View(turno);
EscalonamentoHospitalar/EscalonamentoHospitalar/Controllers/TurnosController.cs
        }

        // GET: Turnos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

EscalonamentoHospitalar/EscalonamentoHospitalar/Controllers/MedicosController.cs
            var medico = await _context.Medicos
                .Include(m => m.EspecialidadeMedico)
                .FirstOrDefaultAsync(m => m.MedicoId == id);
            if (medico == null)

            var turno = await _context.Turnos
                .FirstOrDefaultAsync(m => m.TurnoId == id);
            if (turno == null)
EscalonamentoHospitalar/EscalonamentoHospitalar/Controllers/TurnosController.cs
            {
                return NotFound();
            }

            return View(turno);
        }

        // POST: Turnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turno = await _context.Turnos.FindAsync(id);
            _context.Turnos.Remove(turno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnoExists(int id)
        {
            return _context.Turnos.Any(e => e.TurnoId == id);
        }
    }
}
