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
    public class EnfermeirosController : Controller
    {
        private readonly HospitalDbContext _context;

        public EnfermeirosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: Enfermeiros
        public async Task<IActionResult> Index()
        {
            return View(await _context.Enfermeiros.ToListAsync());
        }

        // GET: Enfermeiros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermeiros = await _context.Enfermeiros
                .FirstOrDefaultAsync(m => m.EnfermeiroID == id);
            if (enfermeiros == null)
            {
                return NotFound();
            }

            return View(enfermeiros);
        }

        // GET: Enfermeiros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Enfermeiros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnfermeiroID,NumeroMecanografico,Nome,Especialidade,Contacto,Email,Data_Nascimento,CC,EspecialidadeId")] Enfermeiros enfermeiros)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enfermeiros);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(enfermeiros);
        }

        // GET: Enfermeiros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermeiros = await _context.Enfermeiros.FindAsync(id);
            if (enfermeiros == null)
            {
                return NotFound();
            }
            return View(enfermeiros);
        }

        // POST: Enfermeiros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnfermeiroID,NumeroMecanografico,Nome,Especialidade,Contacto,Email,Data_Nascimento,CC,EspecialidadeId")] Enfermeiros enfermeiros)
        {
            if (id != enfermeiros.EnfermeiroID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enfermeiros);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnfermeirosExists(enfermeiros.EnfermeiroID))
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
            return View(enfermeiros);
        }

        // GET: Enfermeiros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermeiros = await _context.Enfermeiros
                .FirstOrDefaultAsync(m => m.EnfermeiroID == id);
            if (enfermeiros == null)
            {
                return NotFound();
            }

            return View(enfermeiros);
        }

        // POST: Enfermeiros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enfermeiros = await _context.Enfermeiros.FindAsync(id);
            _context.Enfermeiros.Remove(enfermeiros);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnfermeirosExists(int id)
        {
            return _context.Enfermeiros.Any(e => e.EnfermeiroID == id);
        }
    }
}
