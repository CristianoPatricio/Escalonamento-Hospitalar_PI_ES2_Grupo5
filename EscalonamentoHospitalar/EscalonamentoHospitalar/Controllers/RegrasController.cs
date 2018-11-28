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
    public class RegrasController : Controller
    {
        private readonly HospitalDbContext _context;

        public RegrasController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: Regras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Regras.ToListAsync());
        }

        // GET: Regras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regra = await _context.Regras
                .FirstOrDefaultAsync(m => m.RegraId == id);
            if (regra == null)
            {
                return NotFound();
            }

            return View(regra);
        }

        // GET: Regras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Regras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegraId,RegrasEscalonamento,Numero")] Regra regra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(regra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(regra);
        }

        // GET: Regras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regra = await _context.Regras.FindAsync(id);
            if (regra == null)
            {
                return NotFound();
            }
            return View(regra);
        }

        // POST: Regras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegraId,RegrasEscalonamento,Numero")] Regra regra)
        {
            if (id != regra.RegraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(regra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegraExists(regra.RegraId))
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
            return View(regra);
        }

        // GET: Regras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regra = await _context.Regras
                .FirstOrDefaultAsync(m => m.RegraId == id);
            if (regra == null)
            {
                return NotFound();
            }

            return View(regra);
        }

        // POST: Regras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var regra = await _context.Regras.FindAsync(id);
            _context.Regras.Remove(regra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegraExists(int id)
        {
            return _context.Regras.Any(e => e.RegraId == id);
        }
    }
}
