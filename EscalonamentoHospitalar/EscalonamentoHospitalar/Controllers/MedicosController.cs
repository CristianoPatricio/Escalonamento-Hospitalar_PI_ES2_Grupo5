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
            return View(await _context.Medico.ToListAsync());
        }

        // GET: Medicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico
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
            return View();
        }

        // POST: Medicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicoId,NumeroMecanografico,Nome,Email,Contacto,CC,Data_Nascimento")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }

        // GET: Medicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico.FindAsync(id);
            if (medico == null)
            {
                return NotFound();
            }
            return View(medico);
        }

        // POST: Medicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicoId,NumeroMecanografico,Nome,Email,Contacto,CC,Data_Nascimento")] Medico medico)
        {
            if (id != medico.MedicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }

        // GET: Medicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico
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
            var medico = await _context.Medico.FindAsync(id);
            _context.Medico.Remove(medico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoExists(int id)
        {
            return _context.Medico.Any(e => e.MedicoId == id);
        }
    }
}
