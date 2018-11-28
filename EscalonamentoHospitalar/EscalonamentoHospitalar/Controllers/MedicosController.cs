using EscalonamentoHospitalar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Controllers
{
    public class MedicosController : Controller

    {
        private readonly HospitalDbContext _context;

        public MedicosController(HospitalDbContext context)

        {
            _context = context;
        }

        // GET: Medicos
        public async Task<IActionResult> Index()
        {

            var hospitalDbContext = _context.Medicos.Include(m => m.EspecialidadeMedico);
            return View(await hospitalDbContext.ToListAsync());

        }

        // GET: Medicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .Include(m => m.EspecialidadeMedico)
                .FirstOrDefaultAsync(m => m.MedicoId == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // GET: Medicos/Create
        public IActionResult Create()
        {
            ViewData["EspecialidadeMedicoId"] = new SelectList(_context.EspecialidadeMedicos, "EspecialidadeMedicoId", "NomeEspecialidade");
            return View();
        }

        // POST: Medicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicoId,NumeroMecanografico,Nome,Email,Contacto,CC,Data_Nascimento,EspecialidadeMedicoId,Data_Inicio_Servico")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EspecialidadeMedicoId"] = new SelectList(_context.EspecialidadeMedicos, "EspecialidadeMedicoId", "NomeEspecialidade", medico.EspecialidadeMedicoId);
            return View(medico);
        }

        // GET: Medicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos.FindAsync(id);
            if (medico == null)
            { 
                return NotFound();
            }

            ViewData["EspecialidadeMedicoId"] = new SelectList(_context.EspecialidadeMedicos, "EspecialidadeMedicoId", "NomeEspecialidade", medico.EspecialidadeMedicoId);
            return View(medico);
        }

        // POST: Medicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicoId,NumeroMecanografico,Nome,Email,Contacto,CC,Data_Nascimento,EspecialidadeMedicoId,Data_Inicio_Servico")] Medico medico)
        {
            if (id != medico.MedicoId)
            {
                return NotFound();
            }
            {
                try
                {
                    _context.Update(medico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.MedicoId))
                    {
                        return NotFound();
                    }

                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["EspecialidadeMedicoId"] = new SelectList(_context.EspecialidadeMedicos, "EspecialidadeMedicoId", "NomeEspecialidade", medico.EspecialidadeMedicoId);
            return View(medico);
        }
        
        // GET: Medicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
            .Include(m => m.EspecialidadeMedico)
            .FirstOrDefaultAsync(m => m.MedicoId == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
         
        } 

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medico = await _context.Medicos.FindAsync(id);
            _context.Medicos.Remove(medico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoExists(int id)   
        {
            return _context.Medicos.Any(e => e.MedicoId == id);
        }
    }
}