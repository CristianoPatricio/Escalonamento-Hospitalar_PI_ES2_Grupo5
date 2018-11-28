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
    public class EscalaMedicosController : Controller
    {
        private readonly HospitalDbContext _context;

        public EscalaMedicosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: EscalaMedicos
        public async Task<IActionResult> Index()
        {
            var hospitalDbContext = _context.EscalaMedicos.Include(e => e.Medico).Include(e => e.Turno);
            return View(await hospitalDbContext.ToListAsync());
        }

        // GET: EscalaMedicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escalaMedico = await _context.EscalaMedicos
                .Include(e => e.Medico)
                .Include(e => e.Turno)
                .FirstOrDefaultAsync(m => m.EscalaMedicoId == id);
            if (escalaMedico == null)
            {
                return NotFound();
            }

            return View(escalaMedico);
        }

        // GET: EscalaMedicos/Create
        public IActionResult Create()
        {
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "CC");
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "TurnoId", "TurnoId");
            return View();
        }

        // POST: EscalaMedicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EscalaMedicoId,TurnoId,MedicoId")] EscalaMedico escalaMedico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(escalaMedico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "CC", escalaMedico.MedicoId);
            
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "TurnoId", "TurnoId", escalaMedico.TurnoId);
            return View(escalaMedico);
        }

        // GET: EscalaMedicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escalaMedico = await _context.EscalaMedicos.FindAsync(id);
            if (escalaMedico == null)
            {
                return NotFound();
            }
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "CC", escalaMedico.MedicoId);
            
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "TurnoId", "TurnoId", escalaMedico.TurnoId);
            return View(escalaMedico);
        }

        // POST: EscalaMedicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EscalaMedicoId,TurnoId,MedicoId")] EscalaMedico escalaMedico)
        {
            if (id != escalaMedico.EscalaMedicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(escalaMedico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EscalaMedicoExists(escalaMedico.EscalaMedicoId))
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
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "CC", escalaMedico.MedicoId);
            
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "TurnoId", "TurnoId", escalaMedico.TurnoId);
            return View(escalaMedico);
        }

        // GET: EscalaMedicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escalaMedico = await _context.EscalaMedicos
                .Include(e => e.Medico)
             
                .Include(e => e.Turno)
                .FirstOrDefaultAsync(m => m.EscalaMedicoId == id);
            if (escalaMedico == null)
            {
                return NotFound();
            }

            return View(escalaMedico);
        }

        // POST: EscalaMedicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var escalaMedico = await _context.EscalaMedicos.FindAsync(id);
            _context.EscalaMedicos.Remove(escalaMedico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EscalaMedicoExists(int id)
        {
            return _context.EscalaMedicos.Any(e => e.EscalaMedicoId == id);
        }

        // GET: EscalaMedico/GerarEscalaMedico
        public IActionResult GerarEscalaMedico()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GerarEscalaMedico(GerarEscala gerarEscala)
        {
            if (ModelState.IsValid)
            {
              
                          
            }
            return RedirectToAction(nameof(Index));
        }

        /*************************Funções Auxiliares************************/
        private void InsertDataIntoEscalaMedicos(HospitalDbContext db, int numT1, int numT2, int numT3, DateTime data)
        {

        }

        private int[] MedicosIds()
        {
            var medicos = from m in _context.Medicos
                          select m.MedicoId;

            int[] arrMedicos = medicos.ToArray();

            return arrMedicos;
        }



    }
}
