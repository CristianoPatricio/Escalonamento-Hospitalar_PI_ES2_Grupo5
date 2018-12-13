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
    public class HorarioATrocarEnfermeiroesController : Controller
    {
        private readonly HospitalDbContext _context;

        public HorarioATrocarEnfermeiroesController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: HorarioATrocarEnfermeiroes
        public async Task<IActionResult> Index()
        {
            return View(await _context.HorarioATrocarEnfermeiros.ToListAsync());
        }

        // GET: HorarioATrocarEnfermeiroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioATrocarEnfermeiro = await _context.HorarioATrocarEnfermeiros
                .FirstOrDefaultAsync(m => m.HorarioATrocarEnfermeiroId == id);
            if (horarioATrocarEnfermeiro == null)
            {
                return NotFound();
            }

            return View(horarioATrocarEnfermeiro);
        }

        // GET: HorarioATrocarEnfermeiroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HorarioATrocarEnfermeiroes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HorarioATrocarEnfermeiroId,HorarioATrocarId")] HorarioATrocarEnfermeiro horarioATrocarEnfermeiro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horarioATrocarEnfermeiro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(horarioATrocarEnfermeiro);
        }

        // GET: HorarioATrocarEnfermeiroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioATrocarEnfermeiro = await _context.HorarioATrocarEnfermeiros.FindAsync(id);
            if (horarioATrocarEnfermeiro == null)
            {
                return NotFound();
            }
            return View(horarioATrocarEnfermeiro);
        }

        // POST: HorarioATrocarEnfermeiroes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HorarioATrocarEnfermeiroId,HorarioATrocarId")] HorarioATrocarEnfermeiro horarioATrocarEnfermeiro)
        {
            if (id != horarioATrocarEnfermeiro.HorarioATrocarEnfermeiroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horarioATrocarEnfermeiro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorarioATrocarEnfermeiroExists(horarioATrocarEnfermeiro.HorarioATrocarEnfermeiroId))
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
            return View(horarioATrocarEnfermeiro);
        }

        // GET: HorarioATrocarEnfermeiroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioATrocarEnfermeiro = await _context.HorarioATrocarEnfermeiros
                .FirstOrDefaultAsync(m => m.HorarioATrocarEnfermeiroId == id);
            if (horarioATrocarEnfermeiro == null)
            {
                return NotFound();
            }

            return View(horarioATrocarEnfermeiro);
        }

        // POST: HorarioATrocarEnfermeiroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horarioATrocarEnfermeiro = await _context.HorarioATrocarEnfermeiros.FindAsync(id);
            _context.HorarioATrocarEnfermeiros.Remove(horarioATrocarEnfermeiro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorarioATrocarEnfermeiroExists(int id)
        {
            return _context.HorarioATrocarEnfermeiros.Any(e => e.HorarioATrocarEnfermeiroId == id);
        }
    }
}
